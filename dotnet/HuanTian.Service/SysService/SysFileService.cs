namespace HuanTian.Service
{
    public class SysFileService : IDynamicApiController
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SysFileOutput> Upload([FromForm] SysFileInput input)
        {
            int count = 0;
            var output = new SysFileOutput();
            var path = Path.Combine(App.WebHostEnvironment.WebRootPath, "FileCount", input.FilePath, input.File.FileName);
            // 文件名
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(input.File.FileName);
            // 文件扩展名
            var extension = Path.GetExtension(input.File.FileName);

            if (!File.Exists(path.GetParentPath()))
            {
                Directory.CreateDirectory(path.GetParentPath());
            }
            string newFileName = $"{fileNameWithoutExtension}{extension}";
            path = Path.Combine(App.WebHostEnvironment.WebRootPath, "FileCount", input.FilePath, newFileName);
            if (input.Add)
            {
                // 判断否存在同名文件 加上序号
                while (File.Exists(path))
                {
                    count++;
                    newFileName = $"{fileNameWithoutExtension}-{count}{extension}";
                    path = Path.Combine(App.WebHostEnvironment.WebRootPath, "FileCount", input.FilePath, newFileName);
                }
            }

            if (input.Add || !File.Exists(path))
            {
                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await input.File.CopyToAsync(stream);
                }
            }
            output.FileName = Path.GetFileName(path);
            output.FilePath = App.HttpContext.Request.Scheme + @":\\" + Path.Combine(App.HttpContext.Request.Host.Value, "FileCount", input.FilePath, output.FileName);
            return output;
        }
    }
}
