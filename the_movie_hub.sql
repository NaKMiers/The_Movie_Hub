use master
if exists (select *
from sys.databases
where name = 'TheMovieHub')
  drop database TheMovieHub

create database TheMovieHub
go

if exists (select *
from sys.databases
where name = 'TheMovieHub')
use TheMovieHub
go

create table Theater
(
    id char(50) primary key,
    name nvarchar(255) not null,
    address nvarchar(255) not null,
    image nvarchar(255),
    roomAmount int default 0
);

create table Room
(
    id char(50) primary key,
    theaterId char(50) foreign key (theaterId) references Theater(id),
    name nvarchar(255) not null,
    capacity int not null,
);

create table Seat
(
    id char(50) primary key,
    roomId char(50) foreign key (roomId) references Room(id),
    seatRow int not null,
    seatColumn int not null,
    seatType nvarchar(50) default 'local' check (seatType in ('normal', 'vip', 'couple')),
    isAvailable bit default 1,
);

create table Movie
(
    id char(50) primary key,
    title nvarchar(255) not null,
    releaseDate date not null,
    content text,
    director nvarchar(255) not null,
    actors nvarchar(max),
    duration int not null default 0,
    trailerUrl nvarchar(255) not null,
    rating decimal(3,1) not null default 0,
    country nvarchar(100),
    note text,
    banner nvarchar(255),
    img nvarchar(255) not null,
    active bit default 1
);

create table Showtime
(
    id char(50) primary key,
    movieId char(50) foreign key (movieId) references Movie(id),
    roomId char(50) foreign key (roomId) references Room(id),
    theaterId char(50) foreign key (theaterId) references Theater(id),
    time datetime not null,

);

create table [User]
(
    id char(50) primary key,
    firstname nvarchar(100),
    lastname nvarchar(100),
    phone nvarchar(20),
    email nvarchar(255),
    password nvarchar(255),
    authtype nvarchar(50),
    birthday date,
    expended float default 0,
);

create table Ticket
(
    id char(50) primary key,
    showtimeid char(50) foreign key (showtimeid) references Showtime(id),
    seatId char(50) foreign key (seatId) references Seat(id),
    userid char(50) foreign key (userId) references [User](id),
    price decimal(10, 2) not null,
    paymentMethod nvarchar(100),
    createdAt datetime,
);

create table Genre
(
    id char(50) primary key,
    title nvarchar(255) not null
);


create table MovieGenre
(
    id char(50) primary key,
    movieId char(50) foreign key (movieId) references Movie(id),
    genreId char(50) foreign key (genreid) references Genre(id),
);




