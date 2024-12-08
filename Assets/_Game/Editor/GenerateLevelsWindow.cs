using AfterlifeTmp.Game;
using AfterlifeTmp.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp._Game.Editor
{
	public class GenerateLevelsWindow : EditorWindow
	{

        private const string _PATTERNS_PATH = "Assets/_Game/Prefabs/Level/Patterns/";
        private const string _STORE_LEVELS_PATH = "Assets/_Game/ScriptableObjects/Levels/Normals/";

        private const string _NB_OF_LEVELS_TEXT = "Number Of Levels";
        private const string _CONVEYOR_SPEED_RANGE_TEXT = "Min Max Conveyor Speed";
        private const string _TOTAL_DISTANCE_TEXT = "Min Max Total Distance";
        private const string _NB_PATTERNS_TEXT = "Min Max Number of Patterns";
        private const string _DIFFICULTY_LEVELS_TEXT = "Levels Difficulty Curve";
        private const string _GENERATE_BTN_TEXT = "GENERATE";
        private const string _LEVEL_NAME = "LevelSO_{0}.asset";

        private readonly Vector2 _minMaxDifficulty = new Vector2(1, 5);

        private int _nbOfLevelsToGenerate = 100;
        private Vector2 _minMaxConveyorSpeed = new Vector2(1, 10);
        private Vector2 _minMaxTotalDistance = new Vector2(50, 1000);
        private Vector2 _minMaxNbPatterns = new Vector2(10, 50);
        private AnimationCurve _difficultyCurve = new AnimationCurve();

        private Pattern[] _patterns;

        // Add a menu item to open the editor window
        [MenuItem("Window/Levels Generation")]
        public static void ShowWindow()
        {
            // Open the window and set its title
            GetWindowWithRect<GenerateLevelsWindow>(new Rect(0,0,200, 300));
        }
        private void OnEnable()
        {
            LoadPatterns(); 
            
        }
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            try
            {
                _nbOfLevelsToGenerate =EditorGUILayout.IntField(_NB_OF_LEVELS_TEXT, _nbOfLevelsToGenerate);
                _minMaxConveyorSpeed = EditorGUILayout.Vector2Field(_CONVEYOR_SPEED_RANGE_TEXT, _minMaxConveyorSpeed);
                _minMaxTotalDistance = EditorGUILayout.Vector2Field(_TOTAL_DISTANCE_TEXT, _minMaxTotalDistance);
                _minMaxNbPatterns = EditorGUILayout.Vector2Field(_NB_PATTERNS_TEXT, _minMaxNbPatterns);
                _difficultyCurve = EditorGUILayout.CurveField(_DIFFICULTY_LEVELS_TEXT, _difficultyCurve);

                if (GUILayout.Button(_GENERATE_BTN_TEXT))
                    GenerateLevels();
            }
            finally
            {
                EditorGUILayout.EndVertical();  
            }
        }

        private void GenerateLevels()
        {
            Debug.Log("Generate "+  _nbOfLevelsToGenerate);
            LevelSO lLvl;
            float lCurveRatio;
            float lConvSpeed;
            float lLength;
            int lNbPatterns;
            int lCurDifficulty;
            List<Pattern> lPatterns;
            int lMaxNbRatio = _nbOfLevelsToGenerate - 1;
            for (int i = 0; i < _nbOfLevelsToGenerate; ++i)
            {
                lCurveRatio= _difficultyCurve.Evaluate((float)i / lMaxNbRatio);
                lConvSpeed = HelpersTools.GetLerpedFloatFromVec(_minMaxConveyorSpeed, lCurveRatio);
                lLength = HelpersTools.GetLerpedFloatFromVec(_minMaxTotalDistance, lCurveRatio);
                lNbPatterns = Mathf.RoundToInt(HelpersTools.GetLerpedFloatFromVec(_minMaxNbPatterns, lCurveRatio));
                lCurDifficulty = Mathf.RoundToInt(HelpersTools.GetClampedFloatFromVec(lCurveRatio, _minMaxDifficulty));

                lPatterns = GetRandomPatternsBasedOnDifficulty(lCurDifficulty, lNbPatterns);

                lLvl =  new LevelSO(lConvSpeed, lLength, lPatterns);
                AssetDatabase.CreateAsset(lLvl, _STORE_LEVELS_PATH + string.Format(_LEVEL_NAME, i + 1));

            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private List<Pattern> GetRandomPatternsBasedOnDifficulty(int pCurDifficulty, int pNbPatterns)
        {
            List<Pattern> lPatterns = new List<Pattern>();
            int lMin;
            int lDifficultyToPick;
            Pattern[] lCurDiffPatterns;
            for (int i = 0; i < pNbPatterns; ++i)
            {
                // Choose a random min to give a chance to lower difficulty patterns
                lMin = HelpersTools.GetRandomInt((int)(_minMaxDifficulty.x), pCurDifficulty);

                // Choose a random difficulty
                lDifficultyToPick = HelpersTools.GetRandomInt(lMin, pCurDifficulty);

                // Select all patterns where they have a difficulty value equals to what we are searching
                lCurDiffPatterns = _patterns.Where(pattern => pattern.Difficulty ==  lDifficultyToPick).ToArray();

                // Add one of them into the definitive patterns list
                lPatterns.Add(HelpersTools.PickRandomElementInArray(lCurDiffPatterns));
            }

            return lPatterns;
        }

        private void LoadPatterns()
        {

            // Find all asset GUIDs at the specified path
            string[] lGuids = AssetDatabase.FindAssets("", new[] { _PATTERNS_PATH });

            // Convert GUIDs to asset objects
            _patterns = new Pattern[lGuids.Length];
            for (int i = 0; i < lGuids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(lGuids[i]);
                _patterns[i] = AssetDatabase.LoadAssetAtPath<Pattern>(assetPath);
            }
        }
    }
}