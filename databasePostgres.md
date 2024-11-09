-- Таблица для хранения пользователей

CREATE TABLE Users (

    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    username VARCHAR(50) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для хранения категорий диапазонов (числовых)

CREATE TABLE RangeCategories (

    id SERIAL PRIMARY KEY,
    user_id UUID REFERENCES Users(id) ON DELETE CASCADE,
    name VARCHAR(50) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Таблица для хранения пользовательских диапазонов значений
CREATE TABLE UserRanges (

    id SERIAL PRIMARY KEY,
    user_id UUID REFERENCES Users(id) ON DELETE CASCADE,
    category_id INT REFERENCES RangeCategories(id) ON DELETE SET NULL,
    min_value INT NOT NULL,
    max_value INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для хранения фраз-пожеланий на день

CREATE TABLE DailyPhrases (

    id SERIAL PRIMARY KEY,
    phrase TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для хранения пользовательских категорий уведомлений

CREATE TABLE NotificationCategories (

    id SERIAL PRIMARY KEY,
    user_id UUID REFERENCES Users(id) ON DELETE CASCADE,
    name VARCHAR(50) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для хранения уведомлений, которые могут включать мотивирующие сообщения

CREATE TABLE Notifications (

    id SERIAL PRIMARY KEY,
    user_id UUID REFERENCES Users(id) ON DELETE CASCADE,
    category_id INT REFERENCES NotificationCategories(id) ON DELETE SET NULL,
    message TEXT NOT NULL,
    scheduled_time TIMESTAMP,
    is_sent BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для хранения групп вариантов случайного выбора

CREATE TABLE ChoiceGroups (

    id SERIAL PRIMARY KEY,
    user_id UUID REFERENCES Users(id) ON DELETE CASCADE,
    name VARCHAR(50) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для хранения вариантов выбора внутри группы

CREATE TABLE Choices (

    id SERIAL PRIMARY KEY,
    group_id INT REFERENCES ChoiceGroups(id) ON DELETE CASCADE,
    choice TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для отслеживания истории действий пользователей

CREATE TABLE UserActions (

    id SERIAL PRIMARY KEY,
    user_id UUID REFERENCES Users(id) ON DELETE CASCADE,
    action_type VARCHAR(50) NOT NULL,
    action_description TEXT,
    action_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для записи ежедневной активности пользователя (например, получения уведомления)
CREATE TABLE DailyActivity (

    id SERIAL PRIMARY KEY,
    user_id UUID REFERENCES Users(id) ON DELETE CASCADE,
    notification_id INT REFERENCES Notifications(id) ON DELETE SET NULL,
    phrase_id INT REFERENCES DailyPhrases(id) ON DELETE SET NULL,
    activity_date DATE DEFAULT CURRENT_DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- Таблица для случайных ответов, таких как "да" или "нет"
CREATE TABLE RandomAnswers (

    id SERIAL PRIMARY KEY,
    answer TEXT NOT NULL,
    user_id UUID REFERENCES Users(id) ON DELETE CASCADE

);
