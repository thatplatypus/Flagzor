namespace Flagzor.Components
{
    /// <summary>
    /// Provides information about the current feature flag.
    /// </summary>
    public abstract class FeatureFlagStateProvider
    {

        /// <summary>
        /// Asynchronously gets an <see cref="FeatureFlagState"/>.
        /// </summary>
        public abstract Task<FeatureFlagState> GetFeatureFlagState();

        /// <summary>
        /// An event that provides notification when the <see cref="FeatureFlagState"/>
        /// has changed. For example, this event may be raised if a user logs in or out.
        /// </summary>
        public event FeatureFlagStateHandler? FeatureFlagStateChanged;

        /// <summary>
        /// Raises the <see cref="FeatureFlagStateChanged"/> event.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> that supplies the updated <see cref="FeatureFlagState"/>.</param>
        protected void NotifyFeatureFlagStateChanged(Task<FeatureFlagState> task)
        {
            ArgumentNullException.ThrowIfNull(task);

            FeatureFlagStateChanged?.Invoke(task);
        }
    }

    /// <summary>
    /// A handler for the <see cref="FeatureFlagStateProvider.FeatureFlagStateChanged"/> event.
    /// </summary>
    /// <param name="task">A <see cref="Task"/> that supplies the updated <see cref="FeatureFlagState"/>.</param>
    public delegate void FeatureFlagStateHandler(Task<FeatureFlagState> task);

}
