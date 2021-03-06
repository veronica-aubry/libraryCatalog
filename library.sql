USE [library]
GO
/****** Object:  Table [dbo].[authors]    Script Date: 3/2/2016 4:28:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[authors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[authors_books]    Script Date: 3/2/2016 4:28:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[authors_books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL,
	[author_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[books]    Script Date: 3/2/2016 4:28:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[checkouts]    Script Date: 3/2/2016 4:28:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[checkouts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patron_id] [int] NULL,
	[copy_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[copies]    Script Date: 3/2/2016 4:28:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[copies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[authors_books_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[patrons]    Script Date: 3/2/2016 4:28:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[patrons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[authors] ON 

INSERT [dbo].[authors] ([id], [name]) VALUES (5, N'Italo Calvino')
INSERT [dbo].[authors] ([id], [name]) VALUES (6, N'Author')
SET IDENTITY_INSERT [dbo].[authors] OFF
SET IDENTITY_INSERT [dbo].[authors_books] ON 

INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (1, 1, 1)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (2, 2, 2)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (3, 3, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (4, 4, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (5, 5, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (6, 0, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (7, 0, 1)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (8, 0, 1)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (9, 0, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (10, 0, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (11, 0, 0)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (12, 0, 0)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (13, 0, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (14, 0, 0)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (15, 6, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (16, 7, 1)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (17, 8, 1)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (18, 9, 1)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (19, 10, 1)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (20, 11, 0)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (21, 12, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (22, 13, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (23, 14, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (24, 15, 3)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (25, 16, 0)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (26, 17, 0)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (27, 18, 0)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (28, 19, 0)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (29, 20, 4)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (31, 22, 6)
INSERT [dbo].[authors_books] ([id], [book_id], [author_id]) VALUES (30, 21, 5)
SET IDENTITY_INSERT [dbo].[authors_books] OFF
SET IDENTITY_INSERT [dbo].[books] ON 

INSERT [dbo].[books] ([id], [title]) VALUES (21, N'Baron in the Trees')
INSERT [dbo].[books] ([id], [title]) VALUES (22, N'Book2')
SET IDENTITY_INSERT [dbo].[books] OFF
SET IDENTITY_INSERT [dbo].[checkouts] ON 

INSERT [dbo].[checkouts] ([id], [patron_id], [copy_id]) VALUES (1, 2, 29)
INSERT [dbo].[checkouts] ([id], [patron_id], [copy_id]) VALUES (2, 3, 29)
INSERT [dbo].[checkouts] ([id], [patron_id], [copy_id]) VALUES (3, 4, 34)
SET IDENTITY_INSERT [dbo].[checkouts] OFF
SET IDENTITY_INSERT [dbo].[copies] ON 

INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (25, 30)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (26, 30)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (27, 30)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (28, 30)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (29, 21)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (30, 21)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (31, 21)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (32, 21)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (33, 21)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (34, 21)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (35, 21)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (36, 22)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (37, 22)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (38, 21)
INSERT [dbo].[copies] ([id], [authors_books_id]) VALUES (24, 30)
SET IDENTITY_INSERT [dbo].[copies] OFF
SET IDENTITY_INSERT [dbo].[patrons] ON 

INSERT [dbo].[patrons] ([id], [name]) VALUES (1, N'Robert Alley')
INSERT [dbo].[patrons] ([id], [name]) VALUES (2, N'Robert Alley')
INSERT [dbo].[patrons] ([id], [name]) VALUES (3, N'Veronica Alley')
INSERT [dbo].[patrons] ([id], [name]) VALUES (4, N'Alison Vu')
SET IDENTITY_INSERT [dbo].[patrons] OFF
