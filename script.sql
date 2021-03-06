USE [LibraryManager]
GO
SET IDENTITY_INSERT [dbo].[Borrow] ON 

INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (1, N'B000000008', N'MEM0000001', N'LIB000', CAST(N'2020-06-12' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (2, N'B000000013', N'MEM0000001', N'LIB000', CAST(N'2020-06-12' AS Date), 0)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (3, N'B000000017', N'MEM0000001', N'LIB000', CAST(N'2020-06-12' AS Date), 0)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (4, N'B000000005', N'MEM0000002', N'LIB003', CAST(N'2020-06-15' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (5, N'B000000009', N'MEM0000002', N'LIB003', CAST(N'2020-06-15' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (6, N'B000000014', N'MEM0000002', N'LIB001', CAST(N'2020-07-30' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (7, N'B000000020', N'MEM0000002', N'LIB001', CAST(N'2020-07-30' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (8, N'B000000024', N'MEM0000002', N'LIB001', CAST(N'2020-07-30' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (9, N'B000000003', N'MEM0000007', N'LIB004', CAST(N'2020-05-11' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (10, N'B000000007', N'MEM0000007', N'LIB004', CAST(N'2020-05-11' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (11, N'B000000014', N'MEM0000007', N'LIB004', CAST(N'2020-05-11' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (12, N'B000000018', N'MEM0000007', N'LIB002', CAST(N'2020-06-07' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (13, N'B000000023', N'MEM0000007', N'LIB002', CAST(N'2020-06-07' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (14, N'B000000004', N'MEM0000008', N'LIB000', CAST(N'2020-04-07' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (15, N'B000000010', N'MEM0000008', N'LIB000', CAST(N'2020-04-07' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (16, N'B000000017', N'MEM0000008', N'LIB000', CAST(N'2020-04-07' AS Date), 0)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (17, N'B000000021', N'MEM0000008', N'LIB001', CAST(N'2020-05-10' AS Date), 1)
INSERT [dbo].[Borrow] ([Id], [BookId], [MemberId], [LibrarianId], [BorrowDate], [Status]) VALUES (18, N'B000000024', N'MEM0000008', N'LIB001', CAST(N'2020-05-10' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Borrow] OFF
GO
SET IDENTITY_INSERT [dbo].[ReturnBook] ON 

INSERT [dbo].[ReturnBook] ([Id], [BorrowId], [ReturnDate], [LibrarianId]) VALUES (1, 2, CAST(N'2020-06-20' AS Date), N'LIB001')
INSERT [dbo].[ReturnBook] ([Id], [BorrowId], [ReturnDate], [LibrarianId]) VALUES (2, 3, CAST(N'2020-07-02' AS Date), N'LIB002')
INSERT [dbo].[ReturnBook] ([Id], [BorrowId], [ReturnDate], [LibrarianId]) VALUES (3, 18, CAST(N'2020-05-10' AS Date), N'LIB003')
INSERT [dbo].[ReturnBook] ([Id], [BorrowId], [ReturnDate], [LibrarianId]) VALUES (4, 16, CAST(N'2020-05-10' AS Date), N'LIB004')
SET IDENTITY_INSERT [dbo].[ReturnBook] OFF
GO
