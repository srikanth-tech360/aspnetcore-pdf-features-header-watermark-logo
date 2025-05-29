# ASP.NET Core PDF Generation - Fixed Header Logo - Watremark - Tetx Annotation - Paging 🖨️

## 📚 Table of Contents

- [About the Project](#about-the-project)
- [Features](#features)
- [Getting Started](#getting-started)
- [How to Run](#how-to-run)
- [License](#license)
- [Author](#author)

---

## 📖 About the Project

This repository demonstrates how to generate PDFs from ASP.NET Core MVC Views using **Rotativa.AspNetCore** and **wkhtmltopdf.exe** with a **fixed header logo** across all pages.

✅ Fixed header with logo  
✅ Multi-page PDF generation  
✅ Footer with page numbers
✅ Add Watermark to PDF file
✅ Annotate Permanant text

---

## ✨ Environment and Features

ASP.NET Core MVC project
- Rotativa.AspNetCore integration
- Fixed logo on every page Header
- Page Numbers at footer
- Clean and customizable PDF output
- Appending text to Existing PDF OR  Recreating new PDF with Tetx Annotation
- Add Watermark to existing documnet - Only while dowbloading PDF file or Opening file

---

## 🚀 Getting Started

1. Clone this repository
   ```bash
   git clone https://github.com/srikanth-tech360/aspnetcore-pdf-rotativa-header-logo.git

2. Install Rotativa.AspNetCore via NuGet
  Install-Package Rotativa.AspNetCore
3. Configure the wkhtmltopdf path in Program.cs
  RotativaConfiguration.Setup(env.WebRootPath + "/Rotativa");
4. Run the application and access:
     https://localhost:<port>(44558 etc)/Home/GeneratePdf

## 📜 License
This project is licensed under the MIT License.
See the LICENSE file for details.

🙋‍♂️ Author
Srikanth-Tech360
📧 Connect on LinkedIn - will updtae soon
🚀 Happy Coding!

## 📚 Keywords

ASP.NET Core, PDF Generation, Rotativa.AspNetCore, wkhtmltopdf, Fixed Header Logo, ASP.NET Core MVC, PDF Templates, Arabic PDF generation, Dynamic PDF generation.

## Sample PDF Screesnshots

Screens 1 Hedaer as Logo:

![image](https://github.com/user-attachments/assets/39082a18-2123-41d7-8af3-4d178644d9e1)

![image](https://github.com/user-attachments/assets/0921f3fa-a0ac-4041-8e73-7f4e45f22382)

![image](https://github.com/user-attachments/assets/4c89323e-b2d0-476a-a5d8-91b1ee86d069)


<img width="720" alt="screenshoot1_pdf_Header" src="https://github.com/user-attachments/assets/81760768-941a-4f7f-b62c-d33b8405492c" />

Fotter Paging and Split Page Header



