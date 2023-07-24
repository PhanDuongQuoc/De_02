create database QLSanpham
use QLSanpham


create table LoaiSP
(
	maloai char(2) primary key,
	tenloai nvarchar(30)
)

create table Sanpham
(
	masp char(6) primary key,
	tensp nvarchar(30),
	ngaynhap datetime, 
	maloai char(2),
	constraint pkm_maloai foreign key(maloai) references LoaiSP(maloai)
)