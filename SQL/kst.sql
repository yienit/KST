----------------------------
-- 名称： 考试通数据库脚本(MySQL)
-- 作者： 曹江波
-- 时间： 2016.01.23
----------------------------

-- CREATE DATABASE KST

------------------------------------------ 机构信息 ------------------------------------------

-- 机构信息表 
CREATE TABLE Agency(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
Name VARCHAR(50),						-- 机构名称
State INT DEFAULT 0,					-- 机构状态 (0：正常  1：禁用)
RegTime DATETIME,						-- 注册时间
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

-- 机构管理员信息表
CREATE TABLE AgencyAdmin(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
ChineseName VARCHAR(50),				-- 姓名
Phone VARCHAR(50),						-- 电话号码
Password VARCHAR(50),					-- 密码(默认为电话)
Email VARCHAR(50),						-- 邮箱
Level INT,								-- 账号级别 (0: 题库管理员  1：机构管理员)
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_agencyadmin_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- 机构创建者信息表 
CREATE TABLE AgencyCreator(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
AdminID INT,							-- 机构管理员主键ID
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
CONSTRAINT fk_agencycreator_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- 机构配置信息表 
CREATE TABLE AgencyConfig(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
IsLockDevice INT DEFAULT 0,				-- 是否开启设备锁 (0：不开启  1：开启)
Notice VARCHAR(200),					-- 机构公告
Contact VARCHAR(50),					-- 联系方式
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_agencyconfig_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

------------------------------------------ 课程及章节 ------------------------------------------
-- 系统预设定课程信息表
CREATE TABLE SysCourse(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
Name VARCHAR(50),			    		-- 课程名称
Code VARCHAR(50) UNIQUE,				-- 课程代码
Duration INT,							-- 考试时长 (单位:分钟)
Description VARCHAR(200),				-- 课程描述
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

-- 系统预设定章节信息表
CREATE TABLE SysChapter(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
CourseID INT,							-- 课程主键ID
ChapterIndex INT,						-- 章节序号
Name VARCHAR(50),						-- 章节名称
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_syschapter_syscourse FOREIGN KEY (CourseID) REFERENCES SysCourse(ID) ON DELETE CASCADE
);

-- 课程信息表
CREATE TABLE Course(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
Name VARCHAR(50),			    		-- 课程名称
Code  VARCHAR(50),						-- 课程代码
Duration INT,							-- 考试时长 (单位:分钟)
Description VARCHAR(200),				-- 课程描述
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_course_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- 章节信息表
CREATE TABLE Chapter(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
CourseID INT,							-- 课程主键ID
ChapterIndex INT,						-- 章节序号
Name VARCHAR(50),						-- 章节名称
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_chapter_course FOREIGN KEY (CourseID) REFERENCES Course(ID) ON DELETE CASCADE
);

------------------------------------------ 题库数据 ------------------------------------------

    --********************  题型  *********************
    --*
    --*  1：单选题
    --*  2：多选题
    --*  3：判断题
	--*  4：填空题
	--*  5：不定项选择题
    --*  6：分录题(会计基础专有)
    --*  7：数字填空题(会计基础专有)
    --*
	--************************************************
	
-- 单选题题库
CREATE TABLE SingleItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
ChapterID INT,							-- 章节ID
IsVipItem INT,							-- 是否为VIP题库 (0:普通题库  1：VIP题库)

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(1),							-- 答案
Annotation VARCHAR(500),				-- 注解
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_singleitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_singleitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 多选题题库
CREATE TABLE MultipleItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
ChapterID INT,							-- 章节ID
IsVipItem INT,							-- 是否为VIP题库 (0:普通题库  1：VIP题库)

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(50),						-- 答案 (以中文顿号隔开,如 A、B 或 B、C、D)
Annotation VARCHAR(500),				-- 注解
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_multipleitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_multipleitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 判断题题库
CREATE TABLE JudgeItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
ChapterID INT,							-- 章节ID
IsVipItem INT,							-- 是否为VIP题库 (0:普通题库  1：VIP题库)

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
Answer INT,								-- 答案 (0: 错误  1: 正确)
Annotation VARCHAR(500),				-- 注解
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_judgeitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_judgeitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 不定项选择题
CREATE TABLE UncertainItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
ChapterID INT,							-- 章节ID
IsVipItem INT,							-- 是否为VIP题库 (0:普通题库  1：VIP题库)

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_uncertainitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_uncertainitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 不定项选择题子选择题
CREATE TABLE UncertainSubChoice(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
UncertainItemID INT,					-- 不定项选择题主键ID

ItemIndex INT,							-- 题目序号
Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(20),						-- 答案 (以中文顿号隔开,如 A、B 或 B、C、D)
Annotation VARCHAR(500),				-- 注解

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_uncertainitemsubchoice_uncertainitem FOREIGN KEY (UncertainItemID)REFERENCES UncertainItem(ID) ON DELETE CASCADE
);

-- 分录题题库
CREATE TABLE FenLuItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
ChapterID INT,							-- 章节ID
IsVipItem INT,							-- 是否为VIP题库 (0:普通题库  1：VIP题库)

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_fenluitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_fenluitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 分录题答案
CREATE TABLE FenLuAnswer(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
FenLuItemID INT,						-- 分录题主键ID
Answer VARCHAR(500),					-- 答案
Annotation VARCHAR(500),				-- 注解
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_fenluanswer_fenluitem FOREIGN KEY (FenLuItemID)REFERENCES FenLuItem(ID) ON DELETE CASCADE
);

-- 数字填空题题库
CREATE TABLE NumberBlankItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
ChapterID INT,							-- 章节ID
IsVipItem INT,							-- 是否为VIP题库 (0:普通题库  1：VIP题库)

Title VARCHAR(1000),					-- 标题文字
Image1 MEDIUMBLOB,						-- 图片1(可空)
Image1SubText VARCHAR(1000),			-- 图片1下方文字(可空)
Image2 MEDIUMBLOB,						-- 图片2(可空)
Image2SubText VARCHAR(1000),			-- 图片2下方文字(可空)
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_numberblankitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_numberblankitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 数字填空题答案
CREATE TABLE NumberBlankAnswer(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
NumberBlankItemID INT,					-- 数字填空题主键ID
AnswerName VARCHAR(100),				-- 答案名称
Answer VARCHAR(500),					-- 答案
Annotation VARCHAR(500),				-- 注解
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_numberblankanswer_numberblankitem FOREIGN KEY (NumberBlankItemID)REFERENCES NumberBlankItem(ID) ON DELETE CASCADE
);

------------------------------------------ 试卷数据 ------------------------------------------

-- 试卷信息表
CREATE TABLE Paper(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID
CourseID INT,							-- 课程主键ID
PaperType INT,							-- 试卷类型 (0：模拟试卷  1：历届试卷  2：VIP试卷)
Name VARCHAR(100),						-- 试卷名称
TotalScore FLOAT,						-- 考试总分
Duration INT,							-- 考试时长 (单位:分钟)
AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_paper_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_paper_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- 试卷单选题
CREATE TABLE PaperSingle(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ParperID INT,							-- 试卷主键ID
ItemIndex INT,							-- 试题序号

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(1),							-- 答案
Annotation VARCHAR(500),				-- 注解

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_papersingle_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

-- 试卷多选题
CREATE TABLE PaperMultiple(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ParperID INT,							-- 试卷主键ID
ItemIndex INT,							-- 试题序号

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(50),						-- 答案 (以中文顿号隔开,如 A、B 或 B、C、D)
Annotation VARCHAR(500),				-- 注解

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_papermultiple_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

-- 试卷判断题
CREATE TABLE PaperJudge(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ParperID INT,							-- 试卷主键ID
ItemIndex INT,							-- 试题序号

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
Answer INT,								-- 答案 (0: 错误  1: 正确)
Annotation VARCHAR(500),				-- 注解

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_paperjudge_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

------------------------------------------ 用户数据 ------------------------------------------

-- 机构用户信息表
CREATE TABLE User(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AgencyID INT,							-- 机构主键ID

ChineseName VARCHAR(50),				-- 姓名
Phone VARCHAR(50),						-- 电话号码
Password VARCHAR(50),					-- 密码(默认为电话)
Email VARCHAR(50),						-- 邮箱
Avatar BLOB,							-- 头像图片数据
State INT DEFAULT 0,					-- 账号状态 (0：正常  1：禁用)
IsVip INT DEFAULT 0,					-- 是否为VIP账号(0：普通账号  1：VIP账号)

PcDevcieCode VARCHAR(100),				-- PC设备码
AndroidDeviceCode VARCHAR(100),			-- Android设备码
IosDeviceCode VARCHAR(100),				-- IOS设备码

AddPerson VARCHAR(50),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_user_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- 用户试题评论信息表
CREATE TABLE MyComment(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
CourseID INT,							-- 课程主键ID
ItemType INT,							-- 题型
IsPaperItem INT,						-- 是否为试卷试题 (0：不是  1：是)
ItemID INT,								-- 试题主键ID

UserID INT,								-- 用户账号主键ID
Content VARCHAR(500),					-- 评论内容

AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_mycomment_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_mycomment_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

-- 用户报错题库信息表
CREATE TABLE MyPostErrorItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
CourseID INT,							-- 课程主键ID
ItemType INT,							-- 题型
IsPaperItem INT,						-- 是否为试卷试题 (0：不是  1：是)
ItemID INT,								-- 试题主键ID

UserID INT,								-- 用户账号主键ID
Content VARCHAR(200),					-- 报错内容

AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_myposterroritem_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_myposterroritem_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

-- 用户错题信息表
CREATE TABLE MyError(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
UserID INT,								-- 用户主键ID
CourseID INT,							-- 课程主键ID
ItemType INT,							-- 题型
IsPaperItem INT,						-- 是否为试卷试题 (0：不是  1：是)
ItemID INT,								-- 试题主键ID
Note VARCHAR(200),						-- 用户笔记
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_myerror_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_myerror_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

-- 用户收藏信息表
CREATE TABLE MyFavorite(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
UserID INT,								-- 用户主键ID
CourseID INT,							-- 课程主键ID
ItemType INT,							-- 题型
IsPaperItem INT,						-- 是否为试卷试题 (0：不是  1：是)
ItemID INT,								-- 试题主键ID
Note VARCHAR(100),						-- 用户笔记
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_myfavorite_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_myfavorite_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

-- 用户成绩信息表
CREATE TABLE MyScore(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
UserID INT,								-- 用户主键ID
CourseID INT,							-- 课程主键ID
PaperName INT,							-- 试卷名称
PaperID INT,							-- 试卷主键ID(可空)
Score INT,								-- 分数
UsedTime INT,							-- 用时 (单位分钟)
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_myscore_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_myscore_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

---------------------------------------  记录数据 ------------------------------------------------

-- 短信验证码发送记录
CREATE TABLE CaptchaRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
IP VARCHAR(50),							-- 用户IP地址
CodeType INT,							-- 验证码类型 (0：注册账号验证码  1：找回密码验证码)
Phone VARCHAR(50),						-- 接收验证码的手机
Code VARCHAR(8),						-- 验证码
SendTime DATETIME,						-- 验证码发送时间
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

-- 客户端登录记录
CREATE TABLE ClientLoginRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
UserID INT,								-- 用户主键ID
IP VARCHAR(50),							-- 登录IP
TerminalType INT,						-- 终端类型  (0：PC  1：Android   2: IOS   3: Web)
PlatformVersion VARCHAR(50),			-- 终端所在平台版本，如 Windows7、 Android 4.2.2、 IOS 8.1  、 IE10
AppVersion VARCHAR(50),					-- App版本
LoginTime DATETIME,						-- 登录时间
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

-- 机构管理员操作记录
CREATE TABLE AdminDoRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
AdminID INT,							-- 管理员主键ID
AdminName VARCHAR(50),					-- 管理员姓名
DoTime DATETIME,						-- 操作时间
DoName VARCHAR(100),					-- 操作名称
DoContent VARCHAR(1000),				-- 操作内容
Remark VARCHAR(100),					-- 备注
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

-- 意见反馈信息表
CREATE TABLE FeedbackRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
Content VARCHAR(500),					-- 反馈内容
Contact VARCHAR(50),					-- 联系方式
TerminalType INT,						-- 反馈终端途径 (0：PC  1：Android  2：IOS  3：Web)
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
); 







