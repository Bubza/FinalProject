IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Destinations] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Country] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Destinations] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TourOperators] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [LogoUrl] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_TourOperators] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tours] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [PricePerPerson] decimal(18,2) NOT NULL,
    [DurationDays] int NOT NULL,
    [MaxParticipants] int NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [DestinationId] int NOT NULL,
    [TourOperatorId] int NOT NULL,
    CONSTRAINT [PK_Tours] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tours_Destinations_DestinationId] FOREIGN KEY ([DestinationId]) REFERENCES [Destinations] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Tours_TourOperators_TourOperatorId] FOREIGN KEY ([TourOperatorId]) REFERENCES [TourOperators] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Bookings] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(max) NOT NULL,
    [TourId] int NOT NULL,
    [NumberOfPeople] int NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [Status] int NOT NULL,
    [BookedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookings_Tours_TourId] FOREIGN KEY ([TourId]) REFERENCES [Tours] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(max) NOT NULL,
    [TourId] int NOT NULL,
    [Rating] int NOT NULL,
    [Comment] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reviews_Tours_TourId] FOREIGN KEY ([TourId]) REFERENCES [Tours] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Bookings_TourId] ON [Bookings] ([TourId]);
GO

CREATE INDEX [IX_Reviews_TourId] ON [Reviews] ([TourId]);
GO

CREATE INDEX [IX_Tours_DestinationId] ON [Tours] ([DestinationId]);
GO

CREATE INDEX [IX_Tours_TourOperatorId] ON [Tours] ([TourOperatorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260223163131_AddTourismEntities', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [FavoriteTours] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [TourId] int NOT NULL,
    [AddedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_FavoriteTours] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FavoriteTours_Tours_TourId] FOREIGN KEY ([TourId]) REFERENCES [Tours] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_FavoriteTours_TourId] ON [FavoriteTours] ([TourId]);
GO

CREATE UNIQUE INDEX [IX_FavoriteTours_UserId_TourId] ON [FavoriteTours] ([UserId], [TourId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260304203537_AddFavoriteTours', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [ProfilePictureUrl] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260304211703_AddApplicationUser', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306075037_UpdateSeedData', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Country', N'Description', N'ImageUrl', N'Name') AND [object_id] = OBJECT_ID(N'[Destinations]'))
    SET IDENTITY_INSERT [Destinations] ON;
INSERT INTO [Destinations] ([Id], [Country], [Description], [ImageUrl], [Name])
VALUES (1, N'Италия', N'Вечният град с хиляди години история — Колизеума, Ватикана и прочутата италианска кухня.', N'https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80', N'Рим'),
(2, N'Франция', N'Градът на любовта с Айфеловата кула, Лувъра и незабравимата романтична атмосфера.', N'https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80', N'Париж'),
(3, N'Испания', N'Слънчев крайбрежен град с архитектурата на Гауди, оживения Лас Рамблас и прекрасни плажове.', N'https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80', N'Барселона'),
(4, N'Хърватия', N'Перлата на Адриатика — средновековни стени, кристално синьо море и живописни улички.', N'https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80', N'Дубровник'),
(5, N'Гърция', N'Люлката на цивилизацията — Акрополът, Партенонът и богатата средиземноморска кухня.', N'https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80', N'Атина'),
(6, N'Чехия', N'Градът на сто кули — приказна средновековна архитектура, Карловият мост и бохемска атмосфера.', N'https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80', N'Прага'),
(7, N'Турция', N'Мостът между Европа и Азия — Света София, Гранд базарът и вкусовете на Ориента.', N'https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80', N'Истанбул'),
(8, N'Нидерландия', N'Градът на каналите — велосипеди, лалета, музеи и неповторима холандска архитектура.', N'https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80', N'Амстердам'),
(9, N'Австрия', N'Имперският град — дворци, опера, прочутите виенски кафенета и класическа музика.', N'https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80', N'Виена'),
(10, N'Гърция', N'Вулканичен остров с бели сгради, сини куполи и едни от най-красивите залези в света.', N'https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800&q=80', N'Санторини');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Country', N'Description', N'ImageUrl', N'Name') AND [object_id] = OBJECT_ID(N'[Destinations]'))
    SET IDENTITY_INSERT [Destinations] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Email', N'LogoUrl', N'Name', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[TourOperators]'))
    SET IDENTITY_INSERT [TourOperators] ON;
INSERT INTO [TourOperators] ([Id], [CreatedAt], [Description], [Email], [LogoUrl], [Name], [PhoneNumber])
VALUES (1, '2020-01-01T00:00:00.0000000', N'Водеща туристическа агенция с над 20 години опит в организирането на групови и индивидуални пътувания.', N'info@balkantravel.bg', N'', N'Балкан Травел', N'+359 2 123 4567'),
(2, '2018-06-01T00:00:00.0000000', N'Специалисти в средиземноморския и островен туризъм — море, слънце и незабравими преживявания.', N'contact@suntourism.bg', N'', N'Сън Туризъм', N'+359 2 987 6543'),
(3, '2015-03-15T00:00:00.0000000', N'Групови и индивидуални пътувания до всички европейски дестинации с местни водачи.', N'hello@europaexplorer.bg', N'', N'Европа Експлорър', N'+359 2 555 1234'),
(4, '2017-09-10T00:00:00.0000000', N'Екзотични дестинации, кръстопътни маршрути и незабравими приключения в Азия и Близкия изток.', N'orient@orienttours.bg', N'', N'Ориент Турс', N'+359 2 444 5678'),
(5, '2019-04-20T00:00:00.0000000', N'Специалисти в планински и приключенски туризъм — трекинг, ски и еко пътувания.', N'info@alpinepaths.bg', N'', N'Алпийски Преходи', N'+359 2 777 9900');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Email', N'LogoUrl', N'Name', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[TourOperators]'))
    SET IDENTITY_INSERT [TourOperators] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'DestinationId', N'DurationDays', N'EndDate', N'ImageUrl', N'MaxParticipants', N'PricePerPerson', N'StartDate', N'Title', N'TourOperatorId') AND [object_id] = OBJECT_ID(N'[Tours]'))
    SET IDENTITY_INSERT [Tours] ON;
INSERT INTO [Tours] ([Id], [CreatedAt], [Description], [DestinationId], [DurationDays], [EndDate], [ImageUrl], [MaxParticipants], [PricePerPerson], [StartDate], [Title], [TourOperatorId])
VALUES (1, '2026-01-01T00:00:00.0000000', N'5-дневно пътуване до Вечния град. Посетете Колизеума, Форума, Ватикана и хвърлете монета в Фонтана ди Треви. Включени са квалифициран водач, хотел 4* и закуски.', 1, 5, '2026-04-15T00:00:00.0000000', N'https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80', 20, 899.0, '2026-04-10T00:00:00.0000000', N'Класически Рим', 1),
(2, '2026-01-03T00:00:00.0000000', N'Ексклузивен 3-дневен уикенд тур с влизане без опашка във Ватиканските музеи, частна вечеря в Трастевере и нощувки в бутиков хотел в центъра.', 1, 3, '2026-05-25T00:00:00.0000000', N'https://images.unsplash.com/photo-1568797629192-789acf8e4df3?w=800&q=80', 10, 1250.0, '2026-05-22T00:00:00.0000000', N'Рим и Ватикана — VIP тур', 3),
(3, '2026-01-05T00:00:00.0000000', N'7 дни в Града на светлините — Айфеловата кула, Лувъра, Монмартър и круиз по Сена. Перфектен за двойки и любители на изкуството.', 2, 7, '2026-05-08T00:00:00.0000000', N'https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80', 15, 1199.0, '2026-05-01T00:00:00.0000000', N'Романтичен Париж', 2),
(4, '2026-01-06T00:00:00.0000000', N'Кратък 3-дневен уикенд в Париж — идеален за тези с малко отпуска. Самолет, хотел и основните забележителности включени.', 2, 3, '2026-06-08T00:00:00.0000000', N'https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=800&q=80', 20, 649.0, '2026-06-05T00:00:00.0000000', N'Париж Уикенд', 1),
(5, '2026-01-10T00:00:00.0000000', N'Открийте магията на Барселона с посещения на Саграда Фамилия, парк Гуел, Каса Батло и оживения пазар Бокерия.', 3, 4, '2026-06-19T00:00:00.0000000', N'https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80', 25, 799.0, '2026-06-15T00:00:00.0000000', N'Барселона и Гауди', 3),
(6, '2026-01-15T00:00:00.0000000', N'6 дни в Дубровник и по хърватското крайбрежие. Разходка по крепостните стени, екскурзия до остров Локрум и плаж на Будва.', 4, 6, '2026-07-07T00:00:00.0000000', N'https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80', 18, 699.0, '2026-07-01T00:00:00.0000000', N'Адриатическа перла', 1),
(7, '2026-01-20T00:00:00.0000000', N'Потопете се в историята на Древна Гърция — Акропол, Националният музей, квартал Монастираки и вкусна гръцка вечеря с музика.', 5, 5, '2026-08-15T00:00:00.0000000', N'https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80', 22, 649.0, '2026-08-10T00:00:00.0000000', N'Атинско приключение', 2),
(8, '2026-01-22T00:00:00.0000000', N'4 дни в приказната Прага — Карловият мост, Пражкият замък, Старото градско ядро и легендарните чешки бири в исторически пивници.', 6, 4, '2026-04-22T00:00:00.0000000', N'https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80', 30, 549.0, '2026-04-18T00:00:00.0000000', N'Магията на Прага', 3),
(9, '2026-01-25T00:00:00.0000000', N'Комбиниран тур до две от най-красивите имперски столици — 4 дни в Прага и 4 дни във Виена с автобусен преход и хотели 3*.', 6, 8, '2026-09-13T00:00:00.0000000', N'https://images.unsplash.com/photo-1555576208-f2598fdc0639?w=800&q=80', 25, 980.0, '2026-09-05T00:00:00.0000000', N'Прага и Виена — 8 дни', 1),
(10, '2026-02-01T00:00:00.0000000', N'5 незабравими дни в Истанбул — Света София, Синята джамия, Гранд базарът, круиз по Босфора и автентична турска вечеря с танцьори.', 7, 5, '2026-05-17T00:00:00.0000000', N'https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80', 20, 729.0, '2026-05-12T00:00:00.0000000', N'Истанбул — Мостът на световете', 4),
(11, '2026-02-03T00:00:00.0000000', N'4-дневен тур с круиз по каналите, посещение на музея на Ван Гог и Рийксмузеум, разходка с велосипеди и пазар за цветя.', 8, 4, '2026-04-28T00:00:00.0000000', N'https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80', 18, 819.0, '2026-04-24T00:00:00.0000000', N'Амстердам — Градът на каналите', 3),
(12, '2026-02-05T00:00:00.0000000', N'5 дни в столицата на Хабсбургите — Дворецът Шьонбрун, Операта, Белведере, виенски кафенета и концерт с класическа музика.', 9, 5, '2026-10-08T00:00:00.0000000', N'https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80', 20, 899.0, '2026-10-03T00:00:00.0000000', N'Имперска Виена', 1),
(13, '2026-02-08T00:00:00.0000000', N'7 дни на вулканичния остров Санторини — залезите от Ия, вулканичните плажове, дегустация на местни вина и круиз около острова.', 10, 7, '2026-07-22T00:00:00.0000000', N'https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800&q=80', 14, 1399.0, '2026-07-15T00:00:00.0000000', N'Санторини — Остров на мечтите', 2),
(14, '2026-02-10T00:00:00.0000000', N'3-дневен уикенд полет до Санторини — нощувки в традиционна кикладска вила, гледка към калдерата и частна яхтена обиколка.', 10, 3, '2026-08-31T00:00:00.0000000', N'https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=800&q=80', 8, 850.0, '2026-08-28T00:00:00.0000000', N'Санторини Уикенд', 4),
(15, '2026-02-15T00:00:00.0000000', N'Епичен 10-дневен тур из Балканите — Атина, Солун, Охрид и Будва. Смесица от история, природа и автентична балканска кухня.', 5, 10, '2026-09-30T00:00:00.0000000', N'https://images.unsplash.com/photo-1601599561213-832382fd07ba?w=800&q=80', 28, 1100.0, '2026-09-20T00:00:00.0000000', N'Балкански Експрес — 10 дни', 5);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'DestinationId', N'DurationDays', N'EndDate', N'ImageUrl', N'MaxParticipants', N'PricePerPerson', N'StartDate', N'Title', N'TourOperatorId') AND [object_id] = OBJECT_ID(N'[Tours]'))
    SET IDENTITY_INSERT [Tours] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306075456_UpdatedSeedData', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [Tours] SET [ImageUrl] = N'https://images.unsplash.com/photo-1590147534648-1ac5cca59621?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D'
WHERE [Id] = 15;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260309145544_FixBalkanImage', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [Destinations] SET [Country] = N'Italy', [Description] = N'The Eternal City with thousands of years of history — the Colosseum, the Vatican, and world-renowned Italian cuisine.', [Name] = N'Rome'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'France', [Description] = N'The City of Love — the Eiffel Tower, the Louvre, and an unforgettable romantic atmosphere.', [Name] = N'Paris'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'Spain', [Description] = N'A sun-drenched coastal city with Gaudí''s iconic architecture, vibrant Las Ramblas, and beautiful beaches.', [Name] = N'Barcelona'
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'Croatia', [Description] = N'The Pearl of the Adriatic — medieval walls, crystal-clear sea, and picturesque cobblestone streets.', [Name] = N'Dubrovnik'
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'Greece', [Description] = N'The cradle of civilization — the Acropolis, the Parthenon, and rich Mediterranean cuisine.', [Name] = N'Athens'
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'Czech Republic', [Description] = N'The City of a Hundred Spires — fairy-tale medieval architecture, Charles Bridge, and a bohemian atmosphere.', [Name] = N'Prague'
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'Turkey', [Description] = N'The bridge between Europe and Asia — Hagia Sophia, the Grand Bazaar, and the flavors of the Orient.', [Name] = N'Istanbul'
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'Netherlands', [Description] = N'The City of Canals — bicycles, tulips, world-class museums, and unique Dutch architecture.', [Name] = N'Amsterdam'
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'Austria', [Description] = N'The Imperial City — grand palaces, opera houses, famous Viennese cafés, and classical music.', [Name] = N'Vienna'
WHERE [Id] = 9;
SELECT @@ROWCOUNT;

GO

UPDATE [Destinations] SET [Country] = N'Greece', [Description] = N'A volcanic island with whitewashed buildings, blue domes, and some of the most beautiful sunsets in the world.', [Name] = N'Santorini'
WHERE [Id] = 10;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [Description] = N'A leading travel agency with over 20 years of experience organizing group and individual tours.', [Name] = N'Balkan Travel'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [Description] = N'Specialists in Mediterranean and island tourism — sea, sun, and unforgettable experiences.', [Name] = N'Sun Tourism'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [Description] = N'Group and individual tours to all European destinations with local expert guides.', [Name] = N'Europa Explorer'
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [Description] = N'Exotic destinations, crossroads routes, and unforgettable adventures across Asia and the Middle East.', [Name] = N'Orient Tours'
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [Description] = N'Specialists in mountain and adventure tourism — trekking, skiing, and eco-travel.', [Name] = N'Alpine Paths'
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'A 5-day journey to the Eternal City. Visit the Colosseum, the Roman Forum, the Vatican, and toss a coin in the Trevi Fountain. Includes expert guide, 4* hotel, and breakfast.', [Title] = N'Classic Rome'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'An exclusive 3-day weekend tour with skip-the-line entry to the Vatican Museums, a private dinner in Trastevere, and nights in a boutique hotel in the heart of Rome.', [Title] = N'Rome & Vatican — VIP Tour'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'7 days in the City of Light — the Eiffel Tower, the Louvre, Montmartre, and a cruise along the Seine. Perfect for couples and art lovers.', [Title] = N'Romantic Paris'
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'A short 3-day Paris weekend — ideal for those with limited time off. Flights, hotel, and all major highlights included.', [Title] = N'Paris Weekend'
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'Discover the magic of Barcelona with visits to the Sagrada Família, Park Güell, Casa Batlló, and the vibrant Boqueria market.', [Title] = N'Barcelona & Gaudí'
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'6 days in Dubrovnik and along the Croatian coast. Walk the ancient city walls, take a day trip to Lokrum Island, and relax on the beaches of Budva.', [Title] = N'Adriatic Pearl'
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'Immerse yourself in the history of Ancient Greece — the Acropolis, the National Museum, the Monastiraki district, and a traditional Greek dinner with live music.', [Title] = N'Athens Adventure'
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'4 days in fairy-tale Prague — Charles Bridge, Prague Castle, the Old Town Square, and legendary Czech beers in historic pubs.', [Title] = N'Magic of Prague'
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'A combined tour to two of Europe''s most beautiful imperial capitals — 4 days in Prague and 4 days in Vienna, with coach transfer and 3* hotels.', [Title] = N'Prague & Vienna — 8 Days'
WHERE [Id] = 9;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'5 unforgettable days in Istanbul — Hagia Sophia, the Blue Mosque, the Grand Bazaar, a Bosphorus cruise, and an authentic Turkish dinner with folk dancers.', [Title] = N'Istanbul — Bridge of Worlds'
WHERE [Id] = 10;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'A 4-day tour with a canal cruise, visits to the Van Gogh Museum and Rijksmuseum, a bicycle ride through the city, and a flower market.', [Title] = N'Amsterdam — City of Canals'
WHERE [Id] = 11;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'5 days in the Habsburg capital — Schönbrunn Palace, the State Opera, Belvedere, Viennese cafés, and a classical music concert.', [Title] = N'Imperial Vienna'
WHERE [Id] = 12;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'7 days on the volcanic island of Santorini — sunsets from Oia, volcanic beaches, local wine tasting, and a cruise around the island.', [Title] = N'Santorini — Island of Dreams'
WHERE [Id] = 13;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'A 3-day weekend flight to Santorini — nights in a traditional Cycladic villa overlooking the caldera, and a private yacht tour.', [Title] = N'Santorini Weekend'
WHERE [Id] = 14;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [Description] = N'An epic 10-day tour across the Balkans — Athens, Thessaloniki, Ohrid, and Budva. A blend of history, nature, and authentic Balkan cuisine.', [Title] = N'Balkan Express — 10 Days'
WHERE [Id] = 15;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260310191045_EnglishSeedData', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [Tours] SET [ImageUrl] = N'https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=800&q=80'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [ImageUrl] = N'https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=800&q=80'
WHERE [Id] = 9;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260310191659_ImgUrlsfix', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TourOperators] ADD [UserId] nvarchar(max) NULL;
GO

UPDATE [TourOperators] SET [UserId] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [UserId] = NULL
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [UserId] = NULL
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [UserId] = NULL
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [TourOperators] SET [UserId] = NULL
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260311093440_addUserIdToOperator', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Tours] ADD [DiscountPercent] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 9;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 10;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 11;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 12;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 13;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 14;
SELECT @@ROWCOUNT;

GO

UPDATE [Tours] SET [DiscountPercent] = 0.0
WHERE [Id] = 15;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260314161027_AddDiscounttoTour', N'8.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TourImages] (
    [Id] int NOT NULL IDENTITY,
    [TourId] int NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [SortOrder] int NOT NULL,
    CONSTRAINT [PK_TourImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TourImages_Tours_TourId] FOREIGN KEY ([TourId]) REFERENCES [Tours] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ImageUrl', N'SortOrder', N'TourId') AND [object_id] = OBJECT_ID(N'[TourImages]'))
    SET IDENTITY_INSERT [TourImages] ON;
INSERT INTO [TourImages] ([Id], [ImageUrl], [SortOrder], [TourId])
VALUES (1, N'https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80', 1, 1),
(2, N'https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=1200&q=80', 2, 1),
(3, N'https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=1200&q=80', 3, 1),
(4, N'https://images.unsplash.com/photo-1571366343168-631c5bcca7a4?w=1200&q=80', 4, 1),
(5, N'https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=1200&q=80', 1, 2),
(6, N'https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80', 2, 2),
(7, N'https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?w=1200&q=80', 3, 2),
(8, N'https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=1200&q=80', 1, 3),
(9, N'https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=1200&q=80', 2, 3),
(10, N'https://images.unsplash.com/photo-1541171418693-a55571e5c25f?w=1200&q=80', 3, 3),
(11, N'https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=1200&q=80', 4, 3),
(12, N'https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=1200&q=80', 1, 4),
(13, N'https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=1200&q=80', 2, 4),
(14, N'https://images.unsplash.com/photo-1520939817895-060bdaf4fe1b?w=1200&q=80', 3, 4),
(15, N'https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=1200&q=80', 1, 5),
(16, N'https://images.unsplash.com/photo-1583422409516-2895a77efded?w=1200&q=80', 2, 5),
(17, N'https://images.unsplash.com/photo-1561518776-e76a5e48f731?w=1200&q=80', 3, 5),
(18, N'https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=1200&q=80', 4, 5),
(19, N'https://images.unsplash.com/photo-1555990793-da11153b2473?w=1200&q=80', 1, 6),
(20, N'https://images.unsplash.com/photo-1600240644455-3edc55c375fe?w=1200&q=80', 2, 6),
(21, N'https://images.unsplash.com/photo-1548625149-720834e8fa96?w=1200&q=80', 3, 6),
(22, N'https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80', 1, 7),
(23, N'https://images.unsplash.com/photo-1603565816030-6b389eeb23cb?w=1200&q=80', 2, 7),
(24, N'https://images.unsplash.com/photo-1568515387631-8b650bbcdb90?w=1200&q=80', 3, 7),
(25, N'https://images.unsplash.com/photo-1541726260-e6b6a6a1b27e?w=1200&q=80', 4, 7),
(26, N'https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80', 1, 8),
(27, N'https://images.unsplash.com/photo-1541849546-216549ae216d?w=1200&q=80', 2, 8),
(28, N'https://images.unsplash.com/photo-1592906209472-a36b1f3782ef?w=1200&q=80', 3, 8),
(29, N'https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=1200&q=80', 1, 9),
(30, N'https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80', 2, 9),
(31, N'https://images.unsplash.com/photo-1523731407965-2430cd12f5e4?w=1200&q=80', 3, 9),
(32, N'https://images.unsplash.com/photo-1527838832700-5059252407fa?w=1200&q=80', 1, 10),
(33, N'https://images.unsplash.com/photo-1541432901042-2d8bd64b4a9b?w=1200&q=80', 2, 10),
(34, N'https://images.unsplash.com/photo-1524231757912-21f4fe3a7200?w=1200&q=80', 3, 10),
(35, N'https://images.unsplash.com/photo-1570939274717-7eda259b50ed?w=1200&q=80', 4, 10),
(36, N'https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=1200&q=80', 1, 11),
(37, N'https://images.unsplash.com/photo-1512470876302-972faa2aa9a4?w=1200&q=80', 2, 11),
(38, N'https://images.unsplash.com/photo-1576924542622-772281b13e94?w=1200&q=80', 3, 11),
(39, N'https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=1200&q=80', 1, 12),
(40, N'https://images.unsplash.com/photo-1523731407965-2430cd12f5e4?w=1200&q=80', 2, 12),
(41, N'https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=1200&q=80', 3, 12),
(42, N'https://images.unsplash.com/photo-1609767791926-c9e2e7ca33ac?w=1200&q=80', 4, 12);
INSERT INTO [TourImages] ([Id], [ImageUrl], [SortOrder], [TourId])
VALUES (43, N'https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80', 1, 13),
(44, N'https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80', 2, 13),
(45, N'https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80', 3, 13),
(46, N'https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80', 4, 13),
(47, N'https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80', 1, 14),
(48, N'https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80', 2, 14),
(49, N'https://images.unsplash.com/photo-1551918120-9739cb430c6d?w=1200&q=80', 3, 14),
(50, N'https://images.unsplash.com/photo-1590147534648-1ac5cca59621?w=1200&q=80', 1, 15),
(51, N'https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80', 2, 15),
(52, N'https://images.unsplash.com/photo-1555990793-da11153b2473?w=1200&q=80', 3, 15);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ImageUrl', N'SortOrder', N'TourId') AND [object_id] = OBJECT_ID(N'[TourImages]'))
    SET IDENTITY_INSERT [TourImages] OFF;
GO

CREATE INDEX [IX_TourImages_TourId] ON [TourImages] ([TourId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260322202040_AddTourImages', N'8.0.23');
GO

COMMIT;
GO

