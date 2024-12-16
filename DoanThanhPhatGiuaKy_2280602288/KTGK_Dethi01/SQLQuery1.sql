-- Tạo CSDL QuanLySV
CREATE DATABASE QuanLySV;
GO

-- Sử dụng CSDL QuanLySV
USE QuanLySV;
GO

-- Tạo bảng Lop
CREATE TABLE Lop (
    MaLop CHAR(3) PRIMARY KEY,
    TenLop NVARCHAR(30) NOT NULL
);

-- Tạo bảng Sinhvien
CREATE TABLE Sinhvien (
    MaSV CHAR(6) PRIMARY KEY,
    HotenSV NVARCHAR(40),
    MaLop CHAR(3),
    FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
);

-- Thêm dữ liệu vào bảng Lop
INSERT INTO Lop (MaLop, TenLop)
VALUES 
('L01', N'Công Nghệ Thông Tin'),
('L02', N'Kế Toán Khóa 1');

-- Thêm dữ liệu vào bảng Sinhvien
INSERT INTO Sinhvien (MaSV, HotenSV, MaLop)
VALUES 
('SV0001', N'Trần Văn Nam', 'L01'),
('SV0002', N'Nguyễn Thị Tuyết', 'L02'),
('SV0003', N'Nguyễn Kim Tuyến', 'L02'),
('SV0004', N'Lê Văn Hùng', 'L01'); -- Dòng dữ liệu bị thiếu đã được thêm vào

-- Xem dữ liệu bảng Lop
SELECT * FROM Lop;

-- Xem dữ liệu bảng Sinhvien
SELECT * FROM Sinhvien;


