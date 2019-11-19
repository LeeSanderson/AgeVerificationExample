namespace AgeVerificationExample.Web.Helpers
{
    /// <summary>
    /// Defines the properies of the page buttons rendered in the <see cref="PaginatorTagHelper"/> view templae
    /// </summary>
    public class PageButton
    {
        /// <summary>
        /// Gets or sets the value displayed (and submitted to the server if the button is active)
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the button is active (i.e. the current page).
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the page button is disabled (usually a separator button).
        /// </summary>
        public bool Disabled { get; set; }
    }
}