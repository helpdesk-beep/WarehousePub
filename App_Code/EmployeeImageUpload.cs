using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

public static class EmployeeImageUpload
{
    private const int MaximumImageSize = 5 * 1024 * 1024;

    public static string Save(FileUpload upload, string storedPathPrefix)
    {
        if (upload == null || !upload.HasFile)
        {
            throw new ArgumentException("Please upload an employee image.");
        }

        if (upload.PostedFile.ContentLength <= 0 || upload.PostedFile.ContentLength > MaximumImageSize)
        {
            throw new ArgumentException("Employee images must be smaller than 5 MB.");
        }

        string extension = Path.GetExtension(Path.GetFileName(upload.FileName));
        if (String.IsNullOrEmpty(extension))
        {
            throw new ArgumentException("Only JPG, PNG, or GIF images are allowed.");
        }

        extension = extension.ToLowerInvariant();
        ImageFormat expectedFormat;
        if (extension == ".jpg" || extension == ".jpeg")
        {
            expectedFormat = ImageFormat.Jpeg;
        }
        else if (extension == ".png")
        {
            expectedFormat = ImageFormat.Png;
        }
        else if (extension == ".gif")
        {
            expectedFormat = ImageFormat.Gif;
        }
        else
        {
            throw new ArgumentException("Only JPG, PNG, or GIF images are allowed.");
        }

        using (System.Drawing.Image image = System.Drawing.Image.FromStream(upload.PostedFile.InputStream, false, true))
        {
            if (image.RawFormat.Guid != expectedFormat.Guid)
            {
                throw new ArgumentException("The uploaded file content does not match its image extension.");
            }
        }
        upload.PostedFile.InputStream.Position = 0;

        string fileName = Guid.NewGuid().ToString("N") + extension;
        string physicalDirectory = HttpContext.Current.Server.MapPath("~/Upload/EmployeeImages/");
        Directory.CreateDirectory(physicalDirectory);
        string physicalPath = Path.Combine(physicalDirectory, fileName);

        upload.SaveAs(physicalPath);
        return storedPathPrefix + fileName;
    }
}
