namespace Documents
{
    public static class Document
    {
        public static Document<TMember> FromMember<TMember>(TMember member)
            where TMember : class
        {
            return (Document<TMember>)member;
        }
    }
}
