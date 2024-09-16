namespace TMS.Application.Comments.Dtos
{
    public record CommentDto(int CommentId, string CommentText, string CommentedBy, string email, DateTime CommentDate);
}
