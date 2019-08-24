namespace Validatinator.RuleParsing
{
    internal sealed class EntityReferenceParseResult
    {
        public EntityReferenceParseResult(string error)
        {
            Valid = false;
            Error = error;
        }

        public EntityReferenceParseResult(EntityReference reference)
        {
            Valid = true;
            EntityReference = reference;
        }

        /// <summary>
        /// This will be true unless there was an error parsing the applyif rule.
        /// </summary>
        public bool Valid { get; private set; }

        /// <summary>
        /// The extracted entity reference. Will be null if <see cref="Valid"/> is false.
        /// </summary>
        public EntityReference EntityReference { get; private set; }

        /// <summary>
        /// This will contain an error message if <see cref="Valid"/> is false.
        /// </summary>
        public string Error { get; private set; }
    }
}