#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Monsters.Editor
{
    public static class BuildScript
    {
        public static void BuildDebugAndroid()
        {
            const string outputDir = "Builds/Android";
            const string outputPath = outputDir + "/Monsters-debug.apk";

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            var options = new BuildPlayerOptions
            {
                scenes = new[]
                {
                    "Assets/Scenes/TitleScene.unity",
                    "Assets/Scenes/TownScene.unity",
                    "Assets/Scenes/BattleScene.unity"
                },
                locationPathName = outputPath,
                target = BuildTarget.Android,
                options = BuildOptions.Development
            };

            var report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new BuildFailedException($"Android debug build failed: {report.summary.result}");
            }
        }
    }
}
#endif
