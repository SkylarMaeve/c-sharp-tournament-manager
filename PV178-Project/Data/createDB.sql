CREATE DATABASE PV178_Project;
GO

USE PV178_Project;
GO

    
CREATE TABLE Sport
(
    Id          INT IDENTITY (1,1) PRIMARY KEY,
    Name        NVARCHAR(100) NOT NULL,
    MatchLength INT           NOT NULL
);
GO

CREATE TABLE Tournament
(
    Id         INT IDENTITY (1,1) PRIMARY KEY,
    Name       NVARCHAR(255) NOT NULL,
    SportId    INT           NOT NULL,
    Format     INT           NOT NULL,
    Start  DATETIME      NOT NULL,
    [End]    DATETIME      NOT NULL,
    TeamsCount INT           NOT NULL,
    PointsWin  INT           NOT NULL,
    PointsDraw INT           NOT NULL,
    PointsLoss INT           NOT NULL,
    CONSTRAINT FK_Tournament_Sport FOREIGN KEY (SportId)
        REFERENCES Sport (Id)
);
GO

CREATE TABLE Team
(
    Id           INT IDENTITY (1,1) PRIMARY KEY,
    Name         NVARCHAR(255) NOT NULL,
    TournamentId INT           NOT NULL,
    GroupName    NVARCHAR(50)  NOT NULL,
    Wins         INT           NOT NULL DEFAULT 0,
    Losses       INT           NOT NULL DEFAULT 0,
    Draws        INT           NOT NULL DEFAULT 0,
    Points       INT           NOT NULL DEFAULT 0,
    Placement    INT           NULL,
    CONSTRAINT FK_Team_Tournament FOREIGN KEY (TournamentId)
        REFERENCES Tournament (Id)
);
GO

CREATE TABLE Match
(
    Id           INT IDENTITY (1,1) PRIMARY KEY,
    TournamentId INT           NOT NULL,
    Name         NVARCHAR(255) NOT NULL,
    TeamAId      INT           NULL,
    TeamBId      INT           NULL,
    StartTime    DATETIME      NOT NULL,
    WinnerId     INT           NULL,
    PointsTeamA  INT           NOT NULL DEFAULT 0,
    PointsTeamB  INT           NOT NULL DEFAULT 0,
    CONSTRAINT FK_Match_Tournament FOREIGN KEY (TournamentId)
        REFERENCES Tournament (Id),
    CONSTRAINT FK_Match_TeamA FOREIGN KEY (TeamAId)
        REFERENCES Team (Id),
    CONSTRAINT FK_Match_TeamB FOREIGN KEY (TeamBId)
        REFERENCES Team (Id),
    CONSTRAINT FK_Match_Winner FOREIGN KEY (WinnerId)
        REFERENCES Team (Id)
);
GO

CREATE TABLE Player
(
    Id          INT IDENTITY (1,1) PRIMARY KEY,
    Name        NVARCHAR(255) NOT NULL,
    TeamId      INT           NOT NULL,
    DateOfBirth DATETIME      NOT NULL,
    CONSTRAINT FK_Player_Team FOREIGN KEY (TeamId)
        REFERENCES Team (Id)
);
GO