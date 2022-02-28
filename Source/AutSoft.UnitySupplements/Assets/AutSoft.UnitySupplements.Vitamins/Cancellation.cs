#nullable enable
using System.Threading;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Interface that contains a CancellationToken property <see cref="UnityLifetime"/>
    /// </summary>
    public interface ICancellation
    {
        CancellationToken UnityLifetime { get; }
    }

    /// <summary>
    /// Implements <see cref="ICancellation"/>, cancels the token when application is closed or editor is stopped
    /// </summary>
    public sealed class Cancellation : ICancellation
    {
        private readonly CancellationTokenSource _cts = new();

        public Cancellation()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += OnPlaymodeChanged;
#endif
            Application.quitting += OnQuitting;
        }

        public CancellationToken UnityLifetime => _cts.Token;

#if UNITY_EDITOR
        private void OnPlaymodeChanged(UnityEditor.PlayModeStateChange state)
        {
            if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
            {
                UnityEditor.EditorApplication.playModeStateChanged -= OnPlaymodeChanged;
                _cts.Cancel();
            }
        }
#endif

        private void OnQuitting()
        {
            _cts.Cancel();
            _cts.Dispose();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlaymodeChanged;
#endif
            Application.quitting -= OnQuitting;
        }
    }
}
