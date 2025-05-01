CREATE DATABASE canimkutupanem;
USE canimkutupanem;

CREATE TABLE Uye(
tc int primary key,
adsoyad varchar(50) not null,
yas int not null,
cinsiyet varchar(50) not null,
telefon varchar(50) not null,
adres varchar(50) not null,
email varchar(50) not null,
okukitapsayisi int not null
);
 CREATE TABLE Kitap(
 barkodno varchar(50) not null,
 kitapadi varchar(50) not null,
 yazari varchar(50) not null,
 yayinevi varchar(50) not null,
 sayfasayisi varchar(50) not null,
 turu varchar(50) not null,
 stoksayisi int not null,
 rafno varchar(50) not null,
 aciklama varchar(50) not null,
 kayittarihi varchar(50) not null
 );
 
 CREATE TABLE sepet(
 barkodno varchar(50) not null,
 kitapadi varchar(50) not null,
 yazari varchar(50) not null,
 yayinevi varchar(50) not null,
 sayfasayisi varchar(50) not null,
 kitapsayisi int  not null,
 teslimtarihi varchar(50) not null,
 iadetarihi varchar(50) not null
 );
 
 CREATE TABLE EmanetKitaplar(
 tc varchar(50) not null,
 adsoyad varchar(50) not null,
 yas varchar(50) not null,
 telefon varchar(50) not null,
 barkodno varchar(50) not null,
 kitapadi varchar(50) not null,
 yazari varchar(50) not null,
 yayinevi varchar(50) not null,
 sayfasayisi varchar(50) not null,
 kitapsayisi int not null,
 teslimtarihi varchar(50) not null,
 iadetarihi varchar(50) not null
 );