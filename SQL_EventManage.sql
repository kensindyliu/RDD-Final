Create database [EventManager]
USE [EventManager]
CREATE TABLE [dbo].[Events](
	[EventID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[EventName] [varchar](255) NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[EventLocation] [varchar](255) NOT NULL)


CREATE TABLE [dbo].[EventRegistrations](
	[RegistrationID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[EventID] [int] NULL,
	[ParticipantName] [varchar](255) NOT NULL,
	[ParticipantEmail] [varchar](255) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	FOREIGN KEY (EventID) REFERENCES Events(EventID))

insert into [dbo].[Events] values ( 'EventName111','2024-03-03 15:00:00.000','Room 111')

insert into [dbo].[EventRegistrations] values (1,'John Smith1','xxx1@xxx.ca', '2024-02-25 15:01:00.000')
insert into [dbo].[EventRegistrations] values (1,'John Smith4','xxx4@xxx.ca', '2024-02-25 15:04:00.000')
insert into [dbo].[EventRegistrations] values (1,'John Smith5','xxx5@xxx.ca', '2024-02-25 15:05:00.000')
insert into [dbo].[EventRegistrations] values (1,'John Smith6','xxx6@xxx.ca', '2024-02-25 15:06:00.000')




CREATE  procedure [dbo].[usp_UpdateEvents]
@eventID int,
@oldEventName varchar(255),
@oldEventDate datetime,
@oldEventLocation varchar(255),
@newEventName varchar(255),
@newEventDate datetime,
@newEventLocation varchar(255),
@Result int OUTPUT
as
begin
BEGIN TRANSACTION
BEGIN TRY
	-- Check the date before updating to prevent multiple edits
	IF EXISTS (SELECT * 
			   FROM Events 
			   WHERE EventName = @oldEventName and [EventDate] = @oldEventDate 
			   and [EventLocation] = @oldEventLocation and EventID = @eventID )
	BEGIN
		update Events
		set EventName = @newEventName,
			[EventDate] = @newEventDate,
			[EventLocation] = @newEventLocation
		where EventID = @eventID
		set @Result = 1   -- when update successfully 
	END
	ELSE
	BEGIN
		set @Result = 0  -- when update failed 
	END
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION; -- Handle the exception or log an error
END CATCH
end
GO



Create or alter procedure usp_DeleteEvent
@eventID int,
@isSucceed bit output
as
begin
	begin try
		begin transaction
		delete from EventRegistrations where EventID = @eventID
		delete from Events where EventID = @eventID
		commit transaction
		set @isSucceed = 1
	end try
	begin catch
		rollback transaction 
		set @isSucceed = 0
	end catch
end


-- Explanation:

-- Events Table:
-- EventID: Primary key for uniquely identifying each event.
-- EventName: Name of the event.
-- EventDate: Date when the event will take place.
-- EventLocation: Location where the event will be held.

-- Registrations Table:
-- RegistrationID: Primary key for uniquely identifying each registration.
-- EventID: Foreign key referencing the EventID in the Events table, establishing a relationship between the two tables.
-- ParticipantName: Name of the participant registering for the event.
-- ParticipantEmail: Email of the participant registering for the event.
-- RegistrationDate: Date and time when the registration took place.

-- Explaination of foreign key: 
-- EventID in the Registrations table is a foreign key that references the EventID column in the Events table. 
-- This establishes a relationship between the two tables, specifically indicating that the EventID column in the 
-- Registrations table is related to the EventID column in the Events table.

-- In simpler terms, the FOREIGN KEY constraint ensures that every value in the EventID column of the Registrations table 
-- must exist in the EventID column of the Events table. This ensures referential integrity, meaning that the data in 
-- the Registrations table is consistent with the data in the Events table.

-- The purpose of using foreign keys is to maintain relationships between tables, prevent inconsistencies, and enable 
-- the enforcement of data integrity rules. It helps in avoiding orphaned records (records in the referencing table 
-- that do not have a corresponding record in the referenced table) and ensures that the data in the database remains 
-- accurate and valid.