﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using BLL;

namespace Bloom
{
    public partial class UploadPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (PhotoUpload.HasFile)
            {
                try
                {
                    Guid g;
                    g = Guid.NewGuid();                 
                    Upload upl = new Upload();
                    upl.UserId = (int)Session["userId"];
                    upl.Title = TitleTextBox.Text;
                    string filename = Path.GetFileName(PhotoUpload.FileName);
                    PhotoUpload.SaveAs(Server.MapPath("~/Uploads/")  + filename);
                    string url = Server.MapPath("~/Uploads/")  + filename;
                    upl.Image = url;
                    upl.Title = TitleTextBox.Text.Trim();
                    int result;
                    try
                    {
                        result = upl.Insert();
                        if (result > 0)
                        {
                            Label1.Text = "Upload status: File uploaded!";
                            CommentSection section = new CommentSection();
                            section.UploadId = result;
                            section.Insert();
                        }
                    }
                    catch
                    {
                        Label1.Text = "Upload status: An error ocurred during the upload process.";
                        Label1.Visible = true;
                    }
                    
                }
                catch
                {
                    Label1.Text = "Upload status: The file could not be uploaded.";
                    Label1.Visible = true;
                }


            }
        }
    }
}