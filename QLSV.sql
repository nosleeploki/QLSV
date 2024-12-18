USE [QLSV]
GO
/****** Object:  Table [dbo].[Chuyen_Nganh]    Script Date: 11/18/2024 11:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chuyen_Nganh](
	[MaChuyenNganh] [int] NOT NULL,
	[TenChuyenNganh] [nvarchar](100) NULL,
	[DaXoa] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChuyenNganh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diem]    Script Date: 11/18/2024 11:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diem](
	[MaDiem] [int] IDENTITY(1,1) NOT NULL,
	[MaSinhVien] [int] NOT NULL,
	[MaLop] [int] NOT NULL,
	[DiemGiuaKy] [float] NULL,
	[DiemCuoiKy] [float] NULL,
	[DiemTongKet] [float] NULL,
	[DiemBaiTap1] [float] NULL,
	[DiemBaiTap2] [float] NULL,
	[DiemLab1] [float] NULL,
	[DiemLab2] [float] NULL,
	[DiemChuyenCan] [float] NULL,
	[Pass] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDiem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ghi_Danh]    Script Date: 11/18/2024 11:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ghi_Danh](
	[MaSinhVien] [int] NOT NULL,
	[MaLop] [int] NOT NULL,
	[SoLanVangMat] [int] NULL,
	[MaDe] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSinhVien] ASC,
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hoc_Ky]    Script Date: 11/18/2024 11:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hoc_Ky](
	[MaHocKy] [int] NOT NULL,
	[TenHocKy] [nvarchar](50) NULL,
	[Nam] [int] NULL,
	[DaXoa] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHocKy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop_Hoc]    Script Date: 11/18/2024 11:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop_Hoc](
	[MaLop] [int] NOT NULL,
	[MaMon] [int] NULL,
	[MaHocKy] [int] NULL,
	[TenLop] [nvarchar](100) NULL,
	[DaXoa] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mon_Hoc]    Script Date: 11/18/2024 11:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mon_Hoc](
	[MaMon] [int] NOT NULL,
	[MaSoMon] [nvarchar](20) NULL,
	[TenMon] [nvarchar](100) NULL,
	[SoTinChi] [int] NULL,
	[MaNhomMon] [int] NULL,
	[SoBuoi] [int] NULL,
	[SoLanVangMatToiDa] [int] NULL,
	[DaXoa] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nhom_Mon_Hoc]    Script Date: 11/18/2024 11:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nhom_Mon_Hoc](
	[MaNhomMon] [int] NOT NULL,
	[TenNhomMon] [nvarchar](100) NULL,
	[DaXoa] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhomMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sinh_Vien]    Script Date: 11/18/2024 11:15:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sinh_Vien](
	[MaSinhVien] [int] NOT NULL,
	[Ho] [nvarchar](50) NULL,
	[Ten] [nvarchar](50) NULL,
	[MaSoSinhVien] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[SoDienThoai] [nvarchar](15) NULL,
	[MaChuyenNganh] [int] NULL,
	[GioiTinh] [char](1) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[CMND] [nvarchar](20) NULL,
	[KhoaHoc] [nvarchar](10) NULL,
	[NgaySinh] [date] NULL,
	[GhiChu] [nvarchar](255) NULL,
	[DaXoa] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Chuyen_Nganh] ([MaChuyenNganh], [TenChuyenNganh], [DaXoa]) VALUES (1, N'Công nghệ thông tin', 0)
INSERT [dbo].[Chuyen_Nganh] ([MaChuyenNganh], [TenChuyenNganh], [DaXoa]) VALUES (2, N'Kinh tế', 0)
INSERT [dbo].[Chuyen_Nganh] ([MaChuyenNganh], [TenChuyenNganh], [DaXoa]) VALUES (3, N'Marketing', 0)
INSERT [dbo].[Chuyen_Nganh] ([MaChuyenNganh], [TenChuyenNganh], [DaXoa]) VALUES (4, N'Ngôn ngữ Anh', 0)
INSERT [dbo].[Chuyen_Nganh] ([MaChuyenNganh], [TenChuyenNganh], [DaXoa]) VALUES (5, N'Toán nâng cao', 0)
INSERT [dbo].[Chuyen_Nganh] ([MaChuyenNganh], [TenChuyenNganh], [DaXoa]) VALUES (6, N'Vận tải', 0)
INSERT [dbo].[Chuyen_Nganh] ([MaChuyenNganh], [TenChuyenNganh], [DaXoa]) VALUES (7, N'Công nghệ thông tin 3', 1)
INSERT [dbo].[Chuyen_Nganh] ([MaChuyenNganh], [TenChuyenNganh], [DaXoa]) VALUES (8, N'Công nghệ thông tin 4', 1)
GO
SET IDENTITY_INSERT [dbo].[Diem] ON 

INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (1, 13, 1, 0, 0, 1, 3, 0, 0, 0, 7, 0)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (2, 5, 1, 0, 3, 1, 0, 0, 0, 0, 7, 0)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (3, 1, 1, 0, 0, 0, 0, 0, 0, 0, 7, 0)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (4, 1, 2, 0, 0, 4, 0, 0, 0, 0, 10, NULL)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (5, 11, 1, 0, 0, 0, 0, 0, 0, 0, 7, 0)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (6, 1, 8, 0, 0, 0, 0, 0, 0, 0, 10, NULL)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (7, 49, 1, 2, 2, 2, 2, 2, 2, 2, 7, 0)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (8, 29, 8, 0, 0, 1, 0, 0, 0, 0, 10, NULL)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (9, 42, 8, 0, 0, 1, 2, 0, 2, 0, 10, NULL)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (10, 28, 3, 0, 0, 0, 0, 0, 0, 0, 10, NULL)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (11, 16, 1, 0, 0, 0, 0, 0, 0, 0, 7, 0)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (12, 31, 1, 7, 9, 7, 7, 7, 7, 7, 4, 0)
INSERT [dbo].[Diem] ([MaDiem], [MaSinhVien], [MaLop], [DiemGiuaKy], [DiemCuoiKy], [DiemTongKet], [DiemBaiTap1], [DiemBaiTap2], [DiemLab1], [DiemLab2], [DiemChuyenCan], [Pass]) VALUES (1006, 13, 9, 7, 7, 6, 7, 7, 7, 4, 0, 1)
SET IDENTITY_INSERT [dbo].[Diem] OFF
GO
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (1, 1, 9, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (1, 2, 0, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (1, 8, 0, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (5, 1, 9, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (11, 1, 9, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (13, 1, 9, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (13, 9, 29, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (16, 1, 9, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (28, 3, 3, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (29, 8, 0, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (31, 1, 17, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (42, 8, 3, NULL)
INSERT [dbo].[Ghi_Danh] ([MaSinhVien], [MaLop], [SoLanVangMat], [MaDe]) VALUES (49, 1, 10, NULL)
GO
INSERT [dbo].[Hoc_Ky] ([MaHocKy], [TenHocKy], [Nam], [DaXoa]) VALUES (1, N'Học kỳ 1', 2024, 0)
INSERT [dbo].[Hoc_Ky] ([MaHocKy], [TenHocKy], [Nam], [DaXoa]) VALUES (2, N'Học kỳ 2', 2024, 0)
INSERT [dbo].[Hoc_Ky] ([MaHocKy], [TenHocKy], [Nam], [DaXoa]) VALUES (3, N'Học kỳ hè', 2024, 0)
INSERT [dbo].[Hoc_Ky] ([MaHocKy], [TenHocKy], [Nam], [DaXoa]) VALUES (4, N'Học kỳ 1', 2025, 0)
INSERT [dbo].[Hoc_Ky] ([MaHocKy], [TenHocKy], [Nam], [DaXoa]) VALUES (5, N'Học kỳ 2', 2025, 0)
GO
INSERT [dbo].[Lop_Hoc] ([MaLop], [MaMon], [MaHocKy], [TenLop], [DaXoa]) VALUES (1, 1, 1, N'Lập trình C# - K64', 0)
INSERT [dbo].[Lop_Hoc] ([MaLop], [MaMon], [MaHocKy], [TenLop], [DaXoa]) VALUES (2, 2, 1, N'Kinh tế vi mô - K64', 0)
INSERT [dbo].[Lop_Hoc] ([MaLop], [MaMon], [MaHocKy], [TenLop], [DaXoa]) VALUES (3, 3, 2, N'Kỹ thuật điện tử - K63', 0)
INSERT [dbo].[Lop_Hoc] ([MaLop], [MaMon], [MaHocKy], [TenLop], [DaXoa]) VALUES (4, 4, 2, N'Nguyên lý kế toán - K63', 0)
INSERT [dbo].[Lop_Hoc] ([MaLop], [MaMon], [MaHocKy], [TenLop], [DaXoa]) VALUES (5, 5, 3, N'Tiếng Anh giao tiếp - K65', 0)
INSERT [dbo].[Lop_Hoc] ([MaLop], [MaMon], [MaHocKy], [TenLop], [DaXoa]) VALUES (8, 2, 1, N'K65', 1)
INSERT [dbo].[Lop_Hoc] ([MaLop], [MaMon], [MaHocKy], [TenLop], [DaXoa]) VALUES (9, 3, 1, N'Điện Tử - K63', 0)
GO
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (1, N'MH001', N'Lập trình C#', 3, 1, 30, 3, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (2, N'MH002', N'Kinh tế vi mô', 4, 2, 32, 2, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (3, N'MH003', N'Kỹ thuật điện tử', 3, 3, 28, 3, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (4, N'MH004', N'Nguyên lý kế toán', 2, 4, 25, 2, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (5, N'MH005', N'Tiếng Anh giao tiếp', 2, 5, 30, 2, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (6, N'MH006', N'Qu?n tr? tài chính', 3, 4, 30, 3, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (7, N'MH007', N'Sinh h?c phân t?', 2, 5, 28, 4, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (8, N'MH008', N'Ti?ng Anh', 1, 4, 30, 3, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (9, N'MH009', N'C?u trúc d? li?u', 4, 3, 32, 2, 0)
INSERT [dbo].[Mon_Hoc] ([MaMon], [MaSoMon], [TenMon], [SoTinChi], [MaNhomMon], [SoBuoi], [SoLanVangMatToiDa], [DaXoa]) VALUES (10, N'MH010', N'Qu?n tr? nhân s?', 3, 7, 30, 3, 0)
GO
INSERT [dbo].[Nhom_Mon_Hoc] ([MaNhomMon], [TenNhomMon], [DaXoa]) VALUES (1, N'Khoa học máy tính', 0)
INSERT [dbo].[Nhom_Mon_Hoc] ([MaNhomMon], [TenNhomMon], [DaXoa]) VALUES (2, N'Kinh tế học', 0)
INSERT [dbo].[Nhom_Mon_Hoc] ([MaNhomMon], [TenNhomMon], [DaXoa]) VALUES (3, N'Thiết kế mạch điện', 0)
INSERT [dbo].[Nhom_Mon_Hoc] ([MaNhomMon], [TenNhomMon], [DaXoa]) VALUES (4, N'Kế toán', 0)
INSERT [dbo].[Nhom_Mon_Hoc] ([MaNhomMon], [TenNhomMon], [DaXoa]) VALUES (5, N'Kỹ năng ngôn ngữ', 0)
INSERT [dbo].[Nhom_Mon_Hoc] ([MaNhomMon], [TenNhomMon], [DaXoa]) VALUES (6, N'Kỹ năng phỏng vấn', 1)
INSERT [dbo].[Nhom_Mon_Hoc] ([MaNhomMon], [TenNhomMon], [DaXoa]) VALUES (7, N'Kỹ năng thực hành', 1)
GO
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (1, N'Nguyễn', N'An', N'MSSV1', N'an.nguyen@example.com', N'0912345678', 1, N'M', N'Hà Nội', N'123456788', N'K64', CAST(N'2002-05-15' AS Date), N'Ghi chú 1', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (2, N'Lê', N'Bình', N'MSSV2', N'binh.le@example.com', N'0912345679', 2, N'M', N'Hồ Chí Minh', N'987654321', N'K63', CAST(N'2001-09-21' AS Date), N'Ghi chú 2', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (3, N'Phạm', N'Cường', N'MSSV3', N'cuong.pham@example.com', N'0912345680', 3, N'M', N'Đà Nẵng', N'192837465', N'K62', CAST(N'2000-12-31' AS Date), N'Ghi chú 3', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (4, N'Trần', N'Dung', N'MSSV4', N'dung.tran@example.com', N'0912345681', 4, N'F', N'Quảng Ninh', N'564738291', N'K61', CAST(N'1999-11-10' AS Date), N'Ghi chú 4', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (5, N'Hoàng', N'Em', N'MSSV5', N'em.hoang@example.com', N'0912345682', 5, N'F', N'Hải Phòng', N'847362514', N'K65', CAST(N'2003-03-20' AS Date), N'Ghi chú 5', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (9, N'Lan', N'Quoc', N'MSSV9', N'lan.quoc9@gmail.com', N'0635284805', 7, N'M', N'Đà Nẵng', N'007777809274', N'K60', CAST(N'2020-06-20' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (10, N'Tran', N'Phu', N'MSSV10', N'tran.phu10@gmail.com', N'0309838383', 5, N'M', N'Hà Giang', N'009906535949', N'K62', CAST(N'2004-05-04' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (11, N'Quyen', N'Gia', N'MSSV11', N'quyen.gia11@gmail.com', N'0304998778', 5, N'F', N'Hà Nội', N'008005559276', N'K61', CAST(N'2019-02-02' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (12, N'Trinh', N'Duy', N'MSSV12', N'trinh.duy12@gmail.com', N'0433154267', 2, N'F', N'Hà Nội', N'005551773339', N'K64', CAST(N'2015-09-07' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (13, N'Trinh', N'Duc', N'MSSV13', N'trinh.duc13@gmail.com', N'0518177828', 8, N'M', N'Hà Giang', N'001071365421', N'K63', CAST(N'2017-02-24' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (14, N'Huynh', N'Gia', N'MSSV14', N'huynh.gia14@gmail.com', N'0694645117', 2, N'F', N'Hồ Chí Minh', N'006889803047', N'K61', CAST(N'1997-06-11' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (15, N'Nguyen', N'Bao', N'MSSV15', N'nguyen.bao15@gmail.com', N'0114404717', 8, N'F', N'Hồ Chí Minh', N'002840024403', N'K60', CAST(N'1998-11-01' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (16, N'Huynh', N'Khoa', N'MSSV16', N'huynh.khoa16@gmail.com', N'0071583094', 5, N'M', N'Hồ Chí Minh', N'002921675444', N'K63', CAST(N'2013-01-16' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (17, N'Bui', N'Quoc', N'MSSV17', N'bui.quoc17@gmail.com', N'0553045303', 4, N'M', N'Hà Giang', N'007881545364', N'K61', CAST(N'2004-06-17' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (18, N'Le', N'Khoa', N'MSSV18', N'le.khoa18@gmail.com', N'0657697357', 4, N'M', N'Hải Dương', N'009602767658', N'K63', CAST(N'1998-03-08' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (21, N'Bui', N'Anh', N'MSSV21', N'bui.anh21@gmail.com', N'0528341175', 7, N'M', N'Phú Thọ', N'001359593737', N'K60', CAST(N'2018-04-16' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (22, N'Thi Lan', N'Anh', N'MSSV22', N'thi lan.anh22@gmail.com', N'0373676720', 8, N'M', N'Hà Nội', N'005138379149', N'K63', CAST(N'2006-03-08' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (23, N'Thi Lan', N'Quang', N'MSSV23', N'thi lan.quang23@gmail.com', N'0074166993', 6, N'M', N'Thái Nguyên', N'006939603763', N'K63', CAST(N'2002-03-18' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (25, N'Vu', N'Quoc', N'MSSV25', N'vu.quoc25@gmail.com', N'0599375915', 7, N'M', N'Đà Nẵng', N'009367150547', N'K64', CAST(N'1997-09-06' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (28, N'Pham', N'Tan', N'MSSV28', N'pham.tan28@gmail.com', N'0823755651', 4, N'M', N'Hải Dương', N'003081523049', N'K60', CAST(N'2012-06-02' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (29, N'Hong', N'Bao', N'MSSV29', N'hong.bao29@gmail.com', N'0247052027', 8, N'F', N'Phú Thọ', N'001507325022', N'K61', CAST(N'2006-01-03' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (31, N'Huynh', N'Quoc', N'MSSV31', N'huynh.quoc31@gmail.com', N'0422890711', 3, N'F', N'Đà Nẵng', N'009801167731', N'K64', CAST(N'2009-07-02' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (32, N'Tran', N'Tan', N'MSSV32', N'tran.tan32@gmail.com', N'0137545415', 3, N'F', N'Đà Nẵng', N'001287764359', N'K60', CAST(N'1996-07-24' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (33, N'Bui', N'Phu', N'MSSV33', N'bui.phu33@gmail.com', N'0804426515', 8, N'F', N'Phú Thọ', N'004612103137', N'K63', CAST(N'2016-04-02' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (35, N'Kien', N'Khoa', N'MSSV35', N'kien.khoa35@gmail.com', N'0322082401', 8, N'F', N'Phú Thọ', N'003484915585', N'K61', CAST(N'2001-07-11' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (36, N'Tran', N'Bao', N'MSSV36', N'tran.bao36@gmail.com', N'0986560919', 3, N'F', N'Hà Nội', N'001006417110', N'K60', CAST(N'2011-01-29' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (37, N'Ngoc', N'Vi', N'MSSV37', N'ngoc.vi37@gmail.com', N'0773985566', 5, N'M', N'Hồ Chí Minh', N'007473434157', N'K63', CAST(N'2002-09-06' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (38, N'Le', N'Anh', N'MSSV38', N'le.anh38@gmail.com', N'0526806703', 1, N'F', N'Hải Phòng', N'008773114398', N'K61', CAST(N'2014-06-24' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (39, N'Pham', N'Anh', N'MSSV39', N'pham.anh39@gmail.com', N'0150272585', 2, N'M', N'Hà Nội', N'001274173313', N'K64', CAST(N'2011-12-28' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (40, N'Thi', N'Vinh', N'MSSV40', N'thi.vinh40@gmail.com', N'0113927563', 2, N'F', N'Đà Nẵng', N'009813710010', N'K64', CAST(N'2004-07-16' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (41, N'Lan', N'Duc', N'MSSV41', N'lan.duc41@gmail.com', N'0189542451', 5, N'F', N'Hải Phòng', N'002829788078', N'K61', CAST(N'2010-10-31' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (42, N'Thi', N'Khoa', N'MSSV42', N'thi.khoa42@gmail.com', N'0033077944', 5, N'F', N'Hồ Chí Minh', N'005791574441', N'K62', CAST(N'1999-04-17' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (43, N'Vu', N'Khoa', N'MSSV43', N'vu.khoa43@gmail.com', N'0598191184', 8, N'M', N'Phú Thọ', N'001864602802', N'K63', CAST(N'2021-03-28' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (44, N'Kien', N'Khoa', N'MSSV44', N'kien.khoa44@gmail.com', N'0770073592', 8, N'F', N'Thái Nguyên', N'009230291717', N'K60', CAST(N'1997-07-12' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (45, N'Tran', N'Quang', N'MSSV45', N'tran.quang45@gmail.com', N'0641233040', 3, N'M', N'Thái Nguyên', N'009011515470', N'K64', CAST(N'2004-12-12' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (48, N'Ngoc', N'Doan', N'MSSV48', N'ngoc.doan48@gmail.com', N'0192732891', 1, N'F', N'Hồ Chí Minh', N'006885030787', N'K61', CAST(N'2002-04-19' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (49, N'Trinh', N'Bao', N'MSSV49', N'trinh.bao49@gmail.com', N'0139183699', 2, N'F', N'Hồ Chí Minh', N'003765360266', N'K64', CAST(N'2019-11-20' AS Date), N'...', 0)
INSERT [dbo].[Sinh_Vien] ([MaSinhVien], [Ho], [Ten], [MaSoSinhVien], [Email], [SoDienThoai], [MaChuyenNganh], [GioiTinh], [DiaChi], [CMND], [KhoaHoc], [NgaySinh], [GhiChu], [DaXoa]) VALUES (50, N'Lê Ngọc', N'Thư', N'SV050', N'thu1@gmail.com', N'0394539358', 1, N'M', N'Hà Giang', N'0904052044', N'K61', CAST(N'2024-11-18' AS Date), N'nope', 0)
GO
ALTER TABLE [dbo].[Chuyen_Nganh] ADD  DEFAULT ((0)) FOR [DaXoa]
GO
ALTER TABLE [dbo].[Diem] ADD  DEFAULT ((0)) FOR [DiemGiuaKy]
GO
ALTER TABLE [dbo].[Diem] ADD  DEFAULT ((0)) FOR [DiemCuoiKy]
GO
ALTER TABLE [dbo].[Diem] ADD  DEFAULT ((0)) FOR [DiemTongKet]
GO
ALTER TABLE [dbo].[Diem] ADD  DEFAULT ((0)) FOR [DiemBaiTap1]
GO
ALTER TABLE [dbo].[Diem] ADD  DEFAULT ((0)) FOR [DiemBaiTap2]
GO
ALTER TABLE [dbo].[Diem] ADD  DEFAULT ((0)) FOR [DiemLab1]
GO
ALTER TABLE [dbo].[Diem] ADD  DEFAULT ((0)) FOR [DiemLab2]
GO
ALTER TABLE [dbo].[Hoc_Ky] ADD  DEFAULT ((0)) FOR [DaXoa]
GO
ALTER TABLE [dbo].[Lop_Hoc] ADD  DEFAULT ((0)) FOR [DaXoa]
GO
ALTER TABLE [dbo].[Mon_Hoc] ADD  DEFAULT ((0)) FOR [DaXoa]
GO
ALTER TABLE [dbo].[Nhom_Mon_Hoc] ADD  DEFAULT ((0)) FOR [DaXoa]
GO
ALTER TABLE [dbo].[Sinh_Vien] ADD  DEFAULT ((0)) FOR [DaXoa]
GO
ALTER TABLE [dbo].[Diem]  WITH CHECK ADD FOREIGN KEY([MaLop])
REFERENCES [dbo].[Lop_Hoc] ([MaLop])
GO
ALTER TABLE [dbo].[Diem]  WITH CHECK ADD FOREIGN KEY([MaSinhVien])
REFERENCES [dbo].[Sinh_Vien] ([MaSinhVien])
GO
ALTER TABLE [dbo].[Ghi_Danh]  WITH CHECK ADD FOREIGN KEY([MaLop])
REFERENCES [dbo].[Lop_Hoc] ([MaLop])
GO
ALTER TABLE [dbo].[Ghi_Danh]  WITH CHECK ADD FOREIGN KEY([MaSinhVien])
REFERENCES [dbo].[Sinh_Vien] ([MaSinhVien])
GO
ALTER TABLE [dbo].[Lop_Hoc]  WITH CHECK ADD FOREIGN KEY([MaHocKy])
REFERENCES [dbo].[Hoc_Ky] ([MaHocKy])
GO
ALTER TABLE [dbo].[Lop_Hoc]  WITH CHECK ADD FOREIGN KEY([MaMon])
REFERENCES [dbo].[Mon_Hoc] ([MaMon])
GO
ALTER TABLE [dbo].[Mon_Hoc]  WITH CHECK ADD FOREIGN KEY([MaNhomMon])
REFERENCES [dbo].[Nhom_Mon_Hoc] ([MaNhomMon])
GO
ALTER TABLE [dbo].[Sinh_Vien]  WITH CHECK ADD FOREIGN KEY([MaChuyenNganh])
REFERENCES [dbo].[Chuyen_Nganh] ([MaChuyenNganh])
GO
