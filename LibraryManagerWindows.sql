CREATE DATABASE LibraryManager
GO

USE LibraryManager
GO


-- Tạo bảng Librarian
CREATE TABLE dbo.Librarian
(
	Id VARCHAR(6) PRIMARY KEY,
	FirstName NVARCHAR(10) NULL,
	LastName NVARCHAR(20) NULL,
	Birthday DATE NULL,
	Sex NVARCHAR(5) NULL,
	SSN VARCHAR(12) NULL,
	Address NVARCHAR(100) NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	Email VARCHAR(30) NULL,

	StartDate  DATE DEFAULT GETDATE() NULL,
	Salary DECIMAL(19, 0) NULL,
	Status BIT DEFAULT 1 NULL
)
GO

--Account for Admin
INSERT INTO dbo.Librarian (Id, LastName,PhoneNumber, Status) VALUES ('LIB000', N'Quản trị viên','', 1)
GO

-- Tạo bảng Member
CREATE TABLE dbo.Member
(
	Id VARCHAR(10) PRIMARY KEY,
	FirstName NVARCHAR(10) NULL,
	LastName NVARCHAR(20) NULL,
	Birthday DATE NULL,
	Sex NVARCHAR(5) NULL,
	SSN VARCHAR(12) NULL,
	Address NVARCHAR(100) NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	Email VARCHAR(30) NULL,

	RegisterDate DATE DEFAULT GETDATE() NULL,
	Status BIT DEFAULT 1 NULL
)
GO

-- Tạo bảng Account
CREATE TABLE dbo.Account
(
	PersonId varchar(10) NOT NULL,
	Username varchar(20) PRIMARY KEY NOT NULL,
	Password varchar(32) NOT NULL,
	AccountType INT DEFAULT 2 NOT NULL	-- 0: admin , 1: librarian, 2: member
)
GO 

--Tạo bảng Author
CREATE TABLE dbo.Author
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	NickName NVARCHAR(40) NULL,
	Status BIT DEFAULT 1 NULL,
)
GO 

-- Tạo bảng Publisher
CREATE TABLE dbo.Publisher
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	PhoneNumber VARCHAR(15) NULL,
	Address NVARCHAR(100) NULL,
	Email VARCHAR(30) NULL,
	Website VARCHAR(40) NULL,
	Status BIT DEFAULT 1 NULL,
)
GO 

-- Tạo bảng BookCategory
CREATE TABLE dbo.BookCategory
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Name NVARCHAR(50) NULL,
	LimitDays INT NULL,
	Status BIT DEFAULT 1 NULL
)
GO 

-- Tạo bảng Book
CREATE TABLE dbo.Book
(
	Id VARCHAR(10) PRIMARY KEY,
	Title NVARCHAR(100) NULL,
	PublisherId INT NOT NULL,
	YearPublish INT NULL,
	BookCategoryId INT NULL,
	PageNumber INT NULL,
	Size VARCHAR(11) NULL,
	Price DECIMAL(19, 0) NULL,
	Status BIT DEFAULT 1 NULL

	FOREIGN KEY(PublisherId) REFERENCES dbo.Publisher(Id),
	FOREIGN KEY(BookCategoryId) REFERENCES dbo.BookCategory(Id)
)
GO 

-- Tạo bảng BookItem
CREATE TABLE dbo.BookItem
(
	BookId VARCHAR(10) PRIMARY KEY,
	Number INT NULL,
	Count INT NULL,
	Status BIT DEFAULT 1 NULL

	FOREIGN KEY(BookId) REFERENCES dbo.Book(Id)
)
GO 


-- Tạo bảng BookAuthor
CREATE TABLE dbo.BookAuthor
(
	BookId VARCHAR(10) NOT NULL,
	AuthorId INT NOT NULL

	PRIMARY KEY(BookId, AuthorId),
	FOREIGN KEY(BookId) REFERENCES dbo.Book(Id),
	FOREIGN KEY(AuthorId) REFERENCES dbo.Author(Id)
)
GO 

-- Tạo bảng Borrow
CREATE TABLE dbo.Borrow
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	BookId VARCHAR(10) NOT NULL,
	MemberId VARCHAR(10) NOT NULL,
	LibrarianId VARCHAR(6) NOT NULL,
	BorrowDate DATE NOT NULL,

	FOREIGN KEY(BookId) REFERENCES dbo.Book(Id),
	FOREIGN KEY(MemberId) REFERENCES dbo.Member(Id),
	FOREIGN KEY(LibrarianId) REFERENCES dbo.Librarian(Id)
)
GO 

-- Tạo bảng Return
CREATE TABLE dbo.ReturnBook
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	BorrowId INT NOT NULL,
	ReturnDate DATE DEFAULT GETDATE() NOT NULL

	FOREIGN KEY(BorrowId) REFERENCES dbo.Borrow(Id)
)
GO 

-- Tạo bảng PayFineInfo
CREATE TABLE dbo.PayFineInfo
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	BorrowId INT NOT NULL,
	Cash DECIMAL(19, 0) NOT NULL

	FOREIGN KEY(BorrowId) REFERENCES dbo.Borrow(Id)
)
GO 


-- Tạo hàm để tự tăng ID
CREATE FUNCTION Func_NextLibrarianId(@lastLibrarianId VARCHAR(6), @preFix VARCHAR(3), @size INT)
RETURNS VARCHAR(6)
AS
	BEGIN
		IF (@lastLibrarianId = '')
			SET @lastLibrarianId = @preFix + REPLICATE(0, @size - LEN(@preFix))

		DECLARE @num_nextLibrarianId INT, @nextLibrarianId VARCHAR(6)

		SET @lastLibrarianId = LTRIM(RTRIM(@lastLibrarianId))
		SET @num_nextLibrarianId = REPLACE(@lastLibrarianId, @preFix,'') + 1
		SET @size = @size - LEN(@preFix)
		SET @nextLibrarianId = @preFix + REPLICATE(0, @size - LEN(@preFix))
		SET @nextLibrarianId = @preFix + RIGHT(REPLICATE(0, @size) + CONVERT(VARCHAR(MAX), @num_nextLibrarianId) ,@size)

		RETURN @nextLibrarianId
	END
GO

CREATE FUNCTION Func_NextMemberId(@lastMemberId VARCHAR(10), @preFix VARCHAR(3), @size INT)
	RETURNS VARCHAR(10)
AS
	BEGIN
		IF (@lastMemberId = '')
			SET @lastMemberId = @preFix + REPLICATE(0, @size - LEN(@preFix))

		DECLARE @num_nextMemberId INT ,@nextMemberId VARCHAR(10)

		SET @lastMemberId = LTRIM(RTRIM(@lastMemberId))
		SET @num_nextMemberId = REPLACE(@lastMemberId, @preFix,'') + 1
		SET @size = @size - LEN(@preFix)
		SET @nextMemberId = @preFix + REPLICATE(0, @size - LEN(@preFix))
		SET @nextMemberId = @preFix + RIGHT(REPLICATE(0, @size) + CONVERT(VARCHAR(MAX), @num_nextMemberId) ,@size)

		RETURN @nextMemberId
	END
GO

CREATE FUNCTION Func_NextBookId(@lastBookId VARCHAR(10), @preFix VARCHAR(3), @size INT)
	RETURNS VARCHAR(10)
AS
	BEGIN
		IF (@lastBookId = '')
			SET @lastBookId = @preFix + REPLICATE(0, @size - LEN(@preFix))

		DECLARE @num_nextBookId INT ,@nextBookId VARCHAR(10)

		SET @lastBookId = LTRIM(RTRIM(@lastBookId))
		SET @num_nextBookId = REPLACE(@lastBookId, @preFix,'') + 1
		SET @size = @size - LEN(@preFix)
		SET @nextBookId = @preFix + REPLICATE(0, @size - LEN(@preFix))
		SET @nextBookId = @preFix + RIGHT(REPLICATE(0, @size) + CONVERT(VARCHAR(MAX), @num_nextBookId) ,@size)

		RETURN @nextBookId
	END
GO

CREATE TRIGGER Trig_InsertLibrarian ON [dbo].[Librarian] FOR INSERT
AS 
	BEGIN
		DECLARE @LibrarianId VARCHAR(6)
		SET @LibrarianId = (SELECT TOP (1) Id FROM dbo.Librarian ORDER BY Id DESC)
		SELECT @LibrarianId = dbo.Func_NextLibrarianId(@LibrarianId, 'LIB', 6)

		UPDATE dbo.Librarian SET Id = @LibrarianId WHERE Id=''

		INSERT INTO dbo.Account (PersonId, Username, Password, AccountType)
		VALUES (@LibrarianId, @LibrarianId, 'e73adf9842e38aab89b6a8b9c8824051', 1)
		--default password 000000
	END
GO

CREATE TRIGGER Trig_InsertMember ON [dbo].[Member] FOR INSERT
AS 
	BEGIN
		DECLARE @MemberId VARCHAR(10)
		SET @MemberId = (SELECT TOP (1) Id FROM dbo.Member ORDER BY Id DESC)
		SELECT @MemberId = dbo.Func_NextMemberId(@MemberId, 'MEM', 10)
		UPDATE dbo.Member SET Id = @MemberId WHERE Id = ''

		INSERT INTO dbo.Account (PersonId, Username, Password, AccountType)
		VALUES (@MemberId, @MemberId, 'e73adf9842e38aab89b6a8b9c8824051', 2)
		--default password 000000
	END
GO

CREATE TRIGGER Trig_InsertBook ON [dbo].[Book] FOR INSERT
AS
	BEGIN
		DECLARE @BookId VARCHAR(10)
		SET @BookId = (SELECT TOP (1) Id FROM dbo.Book ORDER BY Id DESC)
		SELECT @BookId = dbo.Func_NextBookId(@BookId, 'B', 10)
		UPDATE dbo.Book SET Id = @BookId WHERE Id = ''
	END
GO

--Trigger cập nhật số lượng sách khi cho mượn
CREATE TRIGGER Trig_InsertBookItem ON [dbo].[BookItem] FOR INSERT
AS
	BEGIN
		DECLARE @BookId VARCHAR(10)
		SET @BookId = (SELECT TOP (1) BookId FROM dbo.BookItem ORDER BY BookId DESC)
		UPDATE dbo.BookItem SET Count = Number WHERE BookId = @BookId
	END
GO

--Trigger cập nhật số lượng sách khi trả
CREATE TRIGGER Trig_InsertBorrow ON [dbo].[Borrow] AFTER INSERT
AS
	BEGIN
		UPDATE dbo.BookItem SET Count = Count - 1 WHERE BookId = (SELECT Inserted.BookId FROM Inserted)
	End
GO

CREATE TRIGGER Trig_DeleteBorrow ON [dbo].[Borrow] AFTER DELETE
AS
	BEGIN
		UPDATE dbo.BookItem SET Count = Count + 1 WHERE BookId = (SELECT Deleted.BookId FROM Deleted)
	End
GO


--CREATE TRIGGER Trig_InsertBorrow ON [dbo].[Borrow] FOR INSERT
--AS
--	BEGIN
--		DECLARE @BookId VARCHAR(10)
--		SET @BookId = (SELECT TOP (1) BookId FROM dbo.Borrow ORDER BY BookId DESC)
--		UPDATE dbo.BookItem SET Count = Count - 1 WHERE BookId = @BookId
--	End
--GO

--CREATE TRIGGER Trig_DeleteBorrow ON [dbo].[Borrow] FOR DELETE
--AS
--	BEGIN
--		DECLARE @BookId VARCHAR(10)
--		DELETE FROM dbo.Borrow
--			WHERE @BookId in (select BookId from dbo.Borrow);
--		UPDATE dbo.BookItem SET Count = Count + 1 WHERE BookId = @BookId
--	End
--GO

--Create admin account
--	username: admin
--	password: admin
INSERT INTO dbo.Account (PersonId, Username, Password, AccountType )
VALUES ('LIB000', 'admin', 'db69fc039dcbd2962cb4d28f5891aae1', 0)

--inser librarian data
INSERT INTO dbo.Librarian (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, StartDate, Salary)
VALUES ('', N'Hoàng', N'Đỗ Văn', '1992-2-10', N'Nam', '436505215753', N'12 Hoàng Diệu, Q.Thủ Đức', '0967892531', 'vanhoang210@gmail.com', '2018-11-26', 8000000)
INSERT INTO dbo.Librarian (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, StartDate, Salary)
VALUES ('', N'Linh', N'Nguyễn Thùy', '1995-12-25', N'Nữ', '0353255202', N'26/2 Đình Phong Phú, Q9', '09898368458', 'jen.nguyen256@gmail.con', '2019-4-24', 7500000)
INSERT INTO dbo.Librarian (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, StartDate, Salary)
VALUES ('', N'Nhi', N'Dương Yến', '1997-7-27', N'Nữ', '35552246253', N'86/23 Đỗ Xuân Hợp, Q9', '09658793158', 'yenduong1997@outlook.com', '2019-8-12', 6500000)
INSERT INTO dbo.Librarian (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, StartDate, Salary)
VALUES ('', N'Loan', N'Trần Thị Kim', '1999-6-30', N'Nữ', '000225365', N'20 Phan Duy Trinh, Q2', '0987894253', 'kimloannguyen@yahoo.com', '2019-12-31', 7250000)
INSERT INTO dbo.Librarian (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, StartDate, Salary)
VALUES ('', N'Lực', N'Huỳnh Tấn', '1990-5-31', N'Khác', '563251540000', N'120/28 Phan Đăng Lưu, Q.Bình Thạnh', '0967663435', 'lucht@gmail.com', '2020-1-2', 6750000)
INSERT INTO dbo.Librarian (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, StartDate, Salary)
VALUES ('', N'Minh', N'Lê Hoàng', '1997-7-2', N'Nam', '525256352', N'182 Võ Văn Ngân, Q.Thủ Đức', '0967892531', 'lehoangminh97@gmail.com', '2020-5-29', 6000000)


--inser member data
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Lâm', N'Hoàng Minh', '1998-3-12', N'Nam', '', N'', '0887895483', 'eriklam98@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Hân', N'Phan Gia', '2002-5-2', N'Nữ', '', N'', '0987853245', 'giahan0502@yahoo.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Như', N'Trần Bảo', '1999-7-8', N'Nữ', '', N'', '0955215831', 'chimsedinang@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Yến', N'Vũ Hoàng', '1997-6-23', N'Nữ', '', N'', '0736565554', '@vuhoangyen1997gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Đức', N'Lê Tấn', '2000-12-31', N'Nam', '', N'', '0358956516', 'ducle.hoabinh@outlook.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Phúc', N'Võ Hoàng', '1996-2-8', N'Nam', '', N'', '0386626265', 'phucphotoshop@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Hưng', N'Lê Khắc', '2000-1-3', N'Nam', '', N'', '0376262656', 'lehung0103000@outlook.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Ngọc', N'Hồ Thị Minh', '2001-1-20', N'Nữ', '', N'', '0915656626', 'hothiminhngoc@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Minh', N'Trương Hoàng', '2002-12-25', N'Nam', '', N'', '0821232625', 'johnytruong@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Quyên', N'Huỳnh Thanh', '2000-5-17', N'Nữ', '', N'', '0867482632', 'min.huynh@outlook.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Thùy', N'Trần Minh', '2001-8-14', N'Nữ', '', N'', '0965965645', 'tranminhthuy2k1@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Anh', N'Nguyễn Tuấn', '2002-6-21', N'Nam', '', N'', '0722321645', 'tuananhnguye2020@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Hồng', N'Đinh Ánh', '1995-3-26', N'Nữ', '', N'', '0979565666', 'jeciccasdinh@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Ánh', N'Kiều Hồng', '1993-2-15', N'Nữ', '', N'', '0387865555', 'kieuanhhong93@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Anh', N'Nguyễn Kiều', '1997-2-1', N'Nữ', '', N'', '0967565121', 'herakieuanh@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Thảo', N'Trần Thu', '2000-7-18', N'Nữ', '', N'', '0357484451', 'thaott@yahoo.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)
VALUES ('', N'Thành', N'Lâm Minh', '1998-7-6', N'Nam', '', N'', '0887323661', 'thanhlam72@gmail.com', GETDATE())
INSERT INTO dbo.Member (Id, FirstName, LastName, Birthday, Sex, SSN, Address, PhoneNumber, Email, RegisterDate)	
VALUES ('', N'Vy', N'Hồ Thị Tường', '1999-3-2', N'Nữ', '', N'', '0917456556', 'tuongvycanhmong@yahoo.com', GETDATE())

INSERT INTO dbo.Publisher (Name, PhoneNumber, Address, Email, Website) VALUES (N'Phụ Nữ Việt Nam', '02439710717', N'39 Hàng Chuối, Hà Nội', 'truyenthongvaprnxbpn@gmail.com', 'http://nxbphunu.com.vn/')
INSERT INTO dbo.Publisher (Name, PhoneNumber, Address, Email, Website) VALUES (N'Trẻ', '02839316289', N'161B Lý Chính Thắng, Phường 7, Quận 3, Hồ Chí Minh', 'info@ybook.vn', 'https://www.nxbtre.com.vn/')
INSERT INTO dbo.Publisher (Name, PhoneNumber, Address, Email, Website) VALUES (N'Văn học', '02437161518', N'18 Nguyễn Trường Tộ - Ba Đình - Hà Nội', 'info@nxbvanhoc.com.vn', 'https://nxbvanhoc.com.vn/')
INSERT INTO dbo.Publisher (Name, PhoneNumber, Address, Email, Website) VALUES (N'Đại Học Quốc Gia Hà Nội', '02439714896', N'16 Hàng Chuối, Phạm Đình Hổ, Hai Bà Trưng, Hà Nội', 'nhaxuatbandhqghanoi@gmail.com', 'https://press.vnu.edu.vn/')
INSERT INTO dbo.Publisher (Name, PhoneNumber, Address, Email, Website) VALUES (N'Đà Nẵng', '02363812964', N'108 Bạch Đằng, Hải Châu 1, Hải Châu, Đà Nẵng', 'xuatban@nxbdanang.vn', 'https://nxbdanang.vn/')
INSERT INTO dbo.Publisher (Name, PhoneNumber, Address, Email, Website) VALUES (N'Thế Giới', '02838220102', N'7 Nguyễn Thị Minh Khai, Bến Nghé, Quận 1, Hồ Chí Minh', 'thegioi@hn.vnn.vn', 'http://www.thegioipublishers.vn/')
INSERT INTO dbo.Publisher (Name, PhoneNumber, Address, Email, Website) VALUES (N'Tổng Hợp TPHCM', '02838256804', N'62 Nguyễn Thị Minh Khai, Đa Kao, Quận 1, Hồ Chí Minh', 'tonghop@nxbhcm.com.vn', 'https://www.nxbhcm.com.vn/')
INSERT INTO dbo.Publisher (Name, PhoneNumber, Address, Email, Website) VALUES (N'Thanh Niên', '0462631724', N'64 Bà Triệu, Hoàn Kiếm, Hà Nội', 'chinhanhnxbthanhnien@gmail.com', 'https://www.nhaxuatbanthanhnien.vn/')


INSERT INTO dbo.Author (NickName) VALUES (N'Ở Đây Zui Nè')		--1
INSERT INTO dbo.Author (NickName) VALUES (N'Tony Buổi Sáng')	--2
INSERT INTO dbo.Author (NickName) VALUES (N'Paulo Coelho')		--3
INSERT INTO dbo.Author (NickName) VALUES (N'Jorge Amado')		--4
INSERT INTO dbo.Author (NickName) VALUES (N'Ngọc Giao')		--5
INSERT INTO dbo.Author (NickName) VALUES (N'Lê Đình Thanh')		--6
INSERT INTO dbo.Author (NickName) VALUES (N'Nguyễn Việt Anh')		--7
INSERT INTO dbo.Author (NickName) VALUES (N'Võ Quốc Bá Cẩn')		--8
INSERT INTO dbo.Author (NickName) VALUES (N'Trần Quốc Anh')		--9
INSERT INTO dbo.Author (NickName) VALUES (N'Trần Phương')		--10
INSERT INTO dbo.Author (NickName) VALUES (N'Mai Lan Hương')		--11
INSERT INTO dbo.Author (NickName) VALUES (N'Hà Thanh Uyên')		--12
INSERT INTO dbo.Author (NickName) VALUES (N'Mai Thị Tường Vân')		--13
INSERT INTO dbo.Author (NickName) VALUES (N'Kiên Trần')		--14
INSERT INTO dbo.Author (NickName) VALUES (N'Nguyễn Thanh Loan')		--15
INSERT INTO dbo.Author (NickName) VALUES (N'Stacey Riches')		--16
INSERT INTO dbo.Author (NickName) VALUES (N'Claire Luong')		--17
INSERT INTO dbo.Author (NickName) VALUES (N'Trí')		--18
INSERT INTO dbo.Author (NickName) VALUES (N'Trác Nhã')		--19
INSERT INTO dbo.Author (NickName) VALUES (N'Dale Carnegie')		--20

INSERT INTO dbo.BookCategory (Name, LimitDays) VALUES (N'Truyện ngắn - tản văn', 20) --1
INSERT INTO dbo.BookCategory (Name, LimitDays) VALUES (N'Kỹ năng sống', 25)		   --2
INSERT INTO dbo.BookCategory (Name, LimitDays) VALUES (N'Tiểu thuyết', 30)		   --3
INSERT INTO dbo.BookCategory (Name, LimitDays) VALUES (N'Luyện thi ĐH-CĐ', 50)		   --4
INSERT INTO dbo.BookCategory (Name, LimitDays) VALUES (N'Khoa học - công nghệ', 30)		   --5
INSERT INTO dbo.BookCategory (Name, LimitDays) VALUES (N'Tiếng Anh', 30)		   --6

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Vui Vẻ Không Quạu Nha', 1, 2020, 1, 280, '10 x 12', 53820)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000001', 20)
INSERT INTO dbo.BookAuthor ( BookId, AuthorId) VALUES ('B000000001', 1)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Cà Phê Cùng Tony', 2, 2017, 1, 268, '13 x 20', 63000)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000002', 20)
INSERT INTO dbo.BookAuthor ( BookId, AuthorId) VALUES ('B000000002', 2)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Trên Đường Băng', 2, 2017, 2, 308, '13 x 20', 64000)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000003', 15)
INSERT INTO dbo.BookAuthor ( BookId, AuthorId) VALUES ('B000000003', 2)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Nhà Giả Kim', 3, 2017, 3, 224, '13 x 20.5', 55200)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000004', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000004', 3)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Hảo Hán Nơi Trảng Cát', 3, 2017, 3, 380, '14.5 x 20.5', 75000)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000005', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000005', 4)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Quán Gió', 3, 2017, 3, 180, '14 x 20.5', 52800)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000006', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000006', 5)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'GT Phát Triển Ứng Dụng Web', 4, 2019, 5, 340, '16 x 24', 168000)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000007', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000007', 6)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000007', 7)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Sử Dụng AM - GM Để Chứng Minh Bất Đẳng Thức', 4, 2019, 4, 256, '16 x 24', 60000)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000008', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000008', 8)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000008', 9)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'VẺ ĐẸP BẤT ĐẲNG THỨC TRONG CÁC KÌ THI OLYMPIC TOÁN HỌC', 4, 2016, 4, 492, '16 x 24', 95000)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000009', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000009', 8)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000009', 9)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000009', 10)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Giải Thích Ngữ Pháp Tiếng Anh (Bài Tập & Đáp Án)', 5, 2019, 6, 200, '16 x 24', 112500)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000010', 15)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000010', 11)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000010', 12)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Giải Mã Trí Nhớ', 5, 2019, 5, 102, '14.5 x 21', 98500)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000011', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000011', 13)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Cẩm Nang Tự Học Ielts', 6, 2019, 6, 188, '16 x 24', 65000)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000012', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000012', 14)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Ngữ Pháp Tiếng Anh', 5, 2019, 6, 280, '13.5 x 20', 60000)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000013', 15)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000013', 11)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000013', 15)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Little Stories – To Push You Forward', 5, 2018, 6, 192, '11.3 x 17.6', 50050)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000014', 5)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000014', 16)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Little Stories - To Make You A Good Person', 5, 2018, 6, 192, '11.3 x 17.6', 50050)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000015', 5)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000015', 16)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Little Stories - The Best Book For Your Leisure Time', 5, 2018, 6, 192, '11.3 x 17.6', 50050)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000016', 5)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000016', 17)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Little Stories - To Get More Knowledge', 5, 2018, 6, 192, '11.3 x 17.6', 50050)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000017', 5)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000017', 17)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Little Stories - To Have A Nice Day', 5, 2018, 6, 192, '11.3 x 17.6', 50050)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000018', 5)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000018', 16)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Little Stories - To Share With Your Friends', 5, 2018, 6, 192, '11.3 x 17.6', 50050)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000019', 5)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000019', 16)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Little Stories - The Book For Peaceful Nights', 5, 2018, 6, 192, '11.3 x 17.6', 50050)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000020', 5)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000020', 17)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Tự Thương Mình Sau Những Tháng Năm Thương Người', 3, 2019, 1, 248, '13 x 20.5', 58500)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000021', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000021', 18)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Mình Buồn Đủ Rồi, Mình Hạnh Phúc Thôi!', 3, 2020, 1, 224, '13 x 20.5', 71200)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000022', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000022', 18)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Khéo Ăn Nói Sẽ Có Được Thiên Hạ', 3, 2018, 2, 406, '14.5 x 20.5', 82500)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000023', 10)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000023', 19)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Đắc Nhân Tâm', 7, 2018, 2, 320, '14.5 x 20.5', 73500)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000024', 15)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000024', 20)

INSERT INTO dbo.Book (Id, Title, PublisherId, YearPublish, BookCategoryId, PageNumber, Size, Price)
VALUES ('', N'Đá Cuội Hay Kim Cương - Cùng Dale Carnegie Tiến Tới Thành Công', 8, 2018, 2, 248, '14.5 x 20.5', 73500)
INSERT INTO dbo.BookItem (BookId, Number) VALUES ('B000000025', 15)
INSERT INTO dbo.BookAuthor (BookId, AuthorId) VALUES ('B000000025', 20)
GO

--SELECT * FROM dbo.Book, dbo.BookItem
--WHERE Id = BookId
--SELECT * FROM dbo.Account
--SELECT * FROM	dbo.Publisher
--SELECT * FROM dbo.Author

--CREATE VIEW [dbo].[View_Author] AS
--SELECT DT1.AuthorId, DT1.NickName, DT2.NumberOfBook, DT1.BookId, DT1.BookTitle FROM
--	(SELECT A.Id AS [AuthorId], A.NickName, B.Id AS [BookId], B.Title AS [BookTitle] 
--	 FROM dbo.Author AS A INNER JOIN dbo.BookAuthor AS BA ON BA.AuthorId = A.Id
--	 INNER JOIN dbo.Book AS B ON B.Id = BA.BookId) AS DT1
--	INNER JOIN
--	(SELECT AuthorId, COUNT(BookId) AS [NumberOfBook] FROM dbo.BookAuthor GROUP BY AuthorId) AS DT2
--	 ON DT2.AuthorId = DT1.AuthorId
--GO

--CREATE VIEW [dbo].[View_Author] AS
--	SELECT A.Id AS [AuthorId], A.NickName, B.Id AS [BookId], B.Title AS [BookTitle], A.Status
--	FROM dbo.Author AS A INNER JOIN dbo.BookAuthor AS BA ON BA.AuthorId = A.Id
--	INNER JOIN dbo.Book AS B ON B.Id = BA.BookId
--GO

--CREATE VIEW [dbo].[View_AuthorNoBook] AS
--	SELECT Id AS [AuthorId], NickName, Status FROM dbo.Author WHERE dbo.Author.Id NOT IN ( SELECT AuthorID FROM View_Author)
--GO

--CREATE VIEW [dbo].[View_BookCategory] AS
--	SELECT BC.Id, BC.Name, BC.LimitDays, COUNT(dbo.Book.Id) AS [NumberOfBook], BC.Status 
--	FROM dbo.BookCategory AS BC Left JOIN dbo.Book ON Book.BookCategoryId = BC.Id 
--	GROUP BY BC.Id, BC.Name, BC.LimitDays, BC.Status
--GO

--CREATE VIEW [dbo].[View_Publisher] AS
--	SELECT P.Id, P.Name, P.PhoneNumber, P.Address, P.Email, P.Website, COUNT(dbo.Book.Id) AS [NumberOfBook], P.Status
--	FROM dbo.Publisher AS P Left JOIN dbo.Book ON Book.PublisherId = P.Id
--	GROUP BY P.Id, P.Name, P.PhoneNumber, P.Address, P.Email, P.Website, P.Status
--GO


INSERT INTO dbo.Borrow (BookId, MemberId, LibrarianId, BorrowDate)
VALUES ('B000000021', 'MEM0000011', 'LIB001', '2019-12-20')
INSERT INTO dbo.Borrow (BookId, MemberId, LibrarianId, BorrowDate)
VALUES ('B000000022', 'MEM0000011', 'LIB001', '2019-12-20')
INSERT INTO dbo.Borrow (BookId, MemberId, LibrarianId, BorrowDate)
VALUES ('B000000023', 'MEM0000011', 'LIB001', '2019-12-20')
INSERT INTO dbo.Borrow (BookId, MemberId, LibrarianId, BorrowDate)
VALUES ('B000000015', 'MEM0000011', 'LIB000', '2019-12-20')
INSERT INTO dbo.Borrow (BookId, MemberId, LibrarianId, BorrowDate)
VALUES ('B000000011', 'MEM0000011', 'LIB001', '2020-1-20')

INSERT INTO dbo.Borrow (BookId, MemberId, LibrarianId, BorrowDate)
VALUES ('B000000001', 'MEM0000012', 'LIB001', '2019-12-20')


