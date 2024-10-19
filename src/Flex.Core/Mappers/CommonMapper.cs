namespace Flex.Core.Mappers
{
    public class CommonMapper
    {
        public static string GetApprovalStatusToDescription(int status)
        {
            return status switch
            {
                1 => "Hoạt động",
                2 => "Không hoạt động",
                3 => "Chờ duyệt thêm mới",
                4 => "Chờ duyệt sửa",
                5 => "Chờ duyệt xoá",
                _ => "Không xác định"
            };
        }
    }
}
