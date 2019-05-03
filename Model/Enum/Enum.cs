namespace ZigmaWeb.Model.Enum
{
    public enum Sexuality : byte
    {
        Male = 1,
        Female = 2
    }

    public enum TextProfileKey
    {

    }

    public enum BinaryProfileKey : int
    {
        UserProfileImage = 1
    }

    public enum CommentStatus : byte
    {
        Confirmed = 1,
        NotConfirmed = 2
    }

    public enum ContentType : byte
    {
        AuthoredContent = 1,
        TranslatedContent = 2,
        BlogPost = 3
    }

}
