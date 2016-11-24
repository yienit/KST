----------------------------
-- ���ƣ� ����ͨ���ݿ�ű�(MySQL)
-- ���ߣ� �ܽ���
-- ʱ�䣺 2016.01.23
----------------------------

-- CREATE DATABASE KST

------------------------------------------ ������Ϣ ------------------------------------------

-- ������Ϣ�� 
CREATE TABLE Agency(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
Name VARCHAR(50),						-- ��������
State INT DEFAULT 0,					-- ����״̬ (0������  1������)
RegTime DATETIME,						-- ע��ʱ��
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

-- ��������Ա��Ϣ��
CREATE TABLE AgencyAdmin(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
ChineseName VARCHAR(50),				-- ����
Phone VARCHAR(50),						-- �绰����
Password VARCHAR(50),					-- ����(Ĭ��Ϊ�绰)
Email VARCHAR(50),						-- ����
Level INT,								-- �˺ż��� (0: ������Ա  1����������Ա)
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_agencyadmin_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- ������������Ϣ�� 
CREATE TABLE AgencyCreator(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
AdminID INT,							-- ��������Ա����ID
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
CONSTRAINT fk_agencycreator_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- ����������Ϣ�� 
CREATE TABLE AgencyConfig(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
IsLockDevice INT DEFAULT 0,				-- �Ƿ����豸�� (0��������  1������)
Notice VARCHAR(200),					-- ��������
Contact VARCHAR(50),					-- ��ϵ��ʽ
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_agencyconfig_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

------------------------------------------ �γ̼��½� ------------------------------------------
-- ϵͳԤ�趨�γ���Ϣ��
CREATE TABLE SysCourse(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
Name VARCHAR(50),			    		-- �γ�����
Code VARCHAR(50) UNIQUE,				-- �γ̴���
Duration INT,							-- ����ʱ�� (��λ:����)
Description VARCHAR(200),				-- �γ�����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

-- ϵͳԤ�趨�½���Ϣ��
CREATE TABLE SysChapter(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
CourseID INT,							-- �γ�����ID
ChapterIndex INT,						-- �½����
Name VARCHAR(50),						-- �½�����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_syschapter_syscourse FOREIGN KEY (CourseID) REFERENCES SysCourse(ID) ON DELETE CASCADE
);

-- �γ���Ϣ��
CREATE TABLE Course(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
Name VARCHAR(50),			    		-- �γ�����
Code  VARCHAR(50),						-- �γ̴���
Duration INT,							-- ����ʱ�� (��λ:����)
Description VARCHAR(200),				-- �γ�����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_course_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- �½���Ϣ��
CREATE TABLE Chapter(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
CourseID INT,							-- �γ�����ID
ChapterIndex INT,						-- �½����
Name VARCHAR(50),						-- �½�����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_chapter_course FOREIGN KEY (CourseID) REFERENCES Course(ID) ON DELETE CASCADE
);

------------------------------------------ ������� ------------------------------------------

    --********************  ����  *********************
    --*
    --*  1����ѡ��
    --*  2����ѡ��
    --*  3���ж���
	--*  4�������
	--*  5��������ѡ����
    --*  6����¼��(��ƻ���ר��)
    --*  7�����������(��ƻ���ר��)
    --*
	--************************************************
	
-- ��ѡ�����
CREATE TABLE SingleItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
ChapterID INT,							-- �½�ID
IsVipItem INT,							-- �Ƿ�ΪVIP��� (0:��ͨ���  1��VIP���)

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(1),							-- ��
Annotation VARCHAR(500),				-- ע��
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_singleitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_singleitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- ��ѡ�����
CREATE TABLE MultipleItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
ChapterID INT,							-- �½�ID
IsVipItem INT,							-- �Ƿ�ΪVIP��� (0:��ͨ���  1��VIP���)

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(50),						-- �� (�����ĶٺŸ���,�� A��B �� B��C��D)
Annotation VARCHAR(500),				-- ע��
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_multipleitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_multipleitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- �ж������
CREATE TABLE JudgeItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
ChapterID INT,							-- �½�ID
IsVipItem INT,							-- �Ƿ�ΪVIP��� (0:��ͨ���  1��VIP���)

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
Answer INT,								-- �� (0: ����  1: ��ȷ)
Annotation VARCHAR(500),				-- ע��
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_judgeitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_judgeitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- ������ѡ����
CREATE TABLE UncertainItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
ChapterID INT,							-- �½�ID
IsVipItem INT,							-- �Ƿ�ΪVIP��� (0:��ͨ���  1��VIP���)

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_uncertainitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_uncertainitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- ������ѡ������ѡ����
CREATE TABLE UncertainSubChoice(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
UncertainItemID INT,					-- ������ѡ��������ID

ItemIndex INT,							-- ��Ŀ���
Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(20),						-- �� (�����ĶٺŸ���,�� A��B �� B��C��D)
Annotation VARCHAR(500),				-- ע��

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_uncertainitemsubchoice_uncertainitem FOREIGN KEY (UncertainItemID)REFERENCES UncertainItem(ID) ON DELETE CASCADE
);

-- ��¼�����
CREATE TABLE FenLuItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
ChapterID INT,							-- �½�ID
IsVipItem INT,							-- �Ƿ�ΪVIP��� (0:��ͨ���  1��VIP���)

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_fenluitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_fenluitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- ��¼���
CREATE TABLE FenLuAnswer(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
FenLuItemID INT,						-- ��¼������ID
AnswerIndex INT,						-- �����
AnswerType INT,							-- ������  (1����¼��  2����մ�(ֻ��һ�������������))
Direction VARCHAR(10),					-- �������(����)
SubjectName VARCHAR(100),				-- ��ƿ�Ŀ
Money VARCHAR(50),						-- ���(��������Ϊ��մ�ʱ��������򼰻�ƿ�Ŀ��Ч��ֻ�н����Ч)
Annotation VARCHAR(500),				-- ע��
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_fenluanswer_fenluitem FOREIGN KEY (FenLuItemID)REFERENCES FenLuItem(ID) ON DELETE CASCADE
);

-- ������������
CREATE TABLE NumberBlankItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
ChapterID INT,							-- �½�ID
IsVipItem INT,							-- �Ƿ�ΪVIP��� (0:��ͨ���  1��VIP���)

Title VARCHAR(1000),					-- ��������
Image1 MEDIUMBLOB,						-- ͼƬ1(�ɿ�)
Image1SubText VARCHAR(1000),			-- ͼƬ1�·�����(�ɿ�)
Image2 MEDIUMBLOB,						-- ͼƬ2(�ɿ�)
Image2SubText VARCHAR(1000),			-- ͼƬ2�·�����(�ɿ�)
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_numberblankitem_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE,
CONSTRAINT fk_numberblankitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- ����������
CREATE TABLE NumberBlankAnswer(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
NumberBlankItemID INT,					-- �������������ID
AnswerName VARCHAR(100),				-- ������
Answer VARCHAR(500),					-- ��
Annotation VARCHAR(500),				-- ע��
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_numberblankanswer_numberblankitem FOREIGN KEY (NumberBlankItemID)REFERENCES NumberBlankItem(ID) ON DELETE CASCADE
);

------------------------------------------ �Ծ����� ------------------------------------------

-- �Ծ���Ϣ��
CREATE TABLE Paper(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID
CourseID INT,							-- �γ�����ID
PaperType INT,							-- �Ծ����� (0��ģ���Ծ�  1�������Ծ�  2��VIP�Ծ�)
Name VARCHAR(100),						-- �Ծ�����
TotalScore FLOAT,						-- �����ܷ�
Duration INT,							-- ����ʱ�� (��λ:����)
AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_paper_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_paper_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- �Ծ�ѡ��
CREATE TABLE PaperSingle(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ParperID INT,							-- �Ծ�����ID
ItemIndex INT,							-- �������

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(1),							-- ��
Annotation VARCHAR(500),				-- ע��

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_papersingle_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

-- �Ծ��ѡ��
CREATE TABLE PaperMultiple(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ParperID INT,							-- �Ծ�����ID
ItemIndex INT,							-- �������

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(50),						-- �� (�����ĶٺŸ���,�� A��B �� B��C��D)
Annotation VARCHAR(500),				-- ע��

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_papermultiple_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

-- �Ծ��ж���
CREATE TABLE PaperJudge(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ParperID INT,							-- �Ծ�����ID
ItemIndex INT,							-- �������

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
Answer INT,								-- �� (0: ����  1: ��ȷ)
Annotation VARCHAR(500),				-- ע��

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_paperjudge_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

------------------------------------------ �û����� ------------------------------------------

-- �����û���Ϣ��
CREATE TABLE User(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AgencyID INT,							-- ��������ID

ChineseName VARCHAR(50),				-- ����
Phone VARCHAR(50),						-- �绰����
Password VARCHAR(50),					-- ����(Ĭ��Ϊ�绰)
Email VARCHAR(50),						-- ����
Avatar BLOB,							-- ͷ��ͼƬ����
State INT DEFAULT 0,					-- �˺�״̬ (0������  1������)
IsVip INT DEFAULT 0,					-- �Ƿ�ΪVIP�˺�(0����ͨ�˺�  1��VIP�˺�)

PcDevcieCode VARCHAR(100),				-- PC�豸��
AndroidDeviceCode VARCHAR(100),			-- Android�豸��
IosDeviceCode VARCHAR(100),				-- IOS�豸��

AddPerson VARCHAR(50),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_user_agency FOREIGN KEY (AgencyID)REFERENCES Agency(ID) ON DELETE CASCADE
);

-- �û�����������Ϣ��
CREATE TABLE MyComment(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
CourseID INT,							-- �γ�����ID
ItemType INT,							-- ����
IsPaperItem INT,						-- �Ƿ�Ϊ�Ծ����� (0������  1����)
ItemID INT,								-- ��������ID

UserID INT,								-- �û��˺�����ID
Content VARCHAR(500),					-- ��������

AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_mycomment_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_mycomment_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

-- �û����������Ϣ��
CREATE TABLE MyPostErrorItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
CourseID INT,							-- �γ�����ID
ItemType INT,							-- ����
IsPaperItem INT,						-- �Ƿ�Ϊ�Ծ����� (0������  1����)
ItemID INT,								-- ��������ID

UserID INT,								-- �û��˺�����ID
Content VARCHAR(200),					-- ��������

AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_myposterroritem_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_myposterroritem_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

-- �û�������Ϣ��
CREATE TABLE MyError(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
UserID INT,								-- �û�����ID
CourseID INT,							-- �γ�����ID
ItemType INT,							-- ����
IsPaperItem INT,						-- �Ƿ�Ϊ�Ծ����� (0������  1����)
ItemID INT,								-- ��������ID
Note VARCHAR(200),						-- �û��ʼ�
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_myerror_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_myerror_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

-- �û��ղ���Ϣ��
CREATE TABLE MyFavorite(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
UserID INT,								-- �û�����ID
CourseID INT,							-- �γ�����ID
ItemType INT,							-- ����
IsPaperItem INT,						-- �Ƿ�Ϊ�Ծ����� (0������  1����)
ItemID INT,								-- ��������ID
Note VARCHAR(100),						-- �û��ʼ�
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_myfavorite_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_myfavorite_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

-- �û��ɼ���Ϣ��
CREATE TABLE MyScore(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
UserID INT,								-- �û�����ID
CourseID INT,							-- �γ�����ID
PaperName INT,							-- �Ծ�����
PaperID INT,							-- �Ծ�����ID(�ɿ�)
Score INT,								-- ����
UsedTime INT,							-- ��ʱ (��λ����)
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_myscore_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE,
CONSTRAINT fk_myscore_user FOREIGN KEY (UserID)REFERENCES User(ID) ON DELETE CASCADE
);

---------------------------------------  ��¼���� ------------------------------------------------

-- ������֤�뷢�ͼ�¼
CREATE TABLE CaptchaRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
IP VARCHAR(50),							-- �û�IP��ַ
CodeType INT,							-- ��֤������ (0��ע���˺���֤��  1���һ�������֤��)
Phone VARCHAR(50),						-- ������֤����ֻ�
Code VARCHAR(8),						-- ��֤��
SendTime DATETIME,						-- ��֤�뷢��ʱ��
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

-- �ͻ��˵�¼��¼
CREATE TABLE ClientLoginRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
UserID INT,								-- �û�����ID
IP VARCHAR(50),							-- ��¼IP
TerminalType INT,						-- �ն�����  (0��PC  1��Android   2: IOS   3: Web)
PlatformVersion VARCHAR(50),			-- �ն�����ƽ̨�汾���� Windows7�� Android 4.2.2�� IOS 8.1  �� IE10
AppVersion VARCHAR(50),					-- App�汾
LoginTime DATETIME,						-- ��¼ʱ��
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

-- ��������Ա������¼
CREATE TABLE AdminDoRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
AdminID INT,							-- ����Ա����ID
AdminName VARCHAR(50),					-- ����Ա����
DoTime DATETIME,						-- ����ʱ��
DoName VARCHAR(100),					-- ��������
DoContent VARCHAR(1000),				-- ��������
Remark VARCHAR(100),					-- ��ע
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

-- ���������Ϣ��
CREATE TABLE FeedbackRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
Content VARCHAR(500),					-- ��������
Contact VARCHAR(50),					-- ��ϵ��ʽ
TerminalType INT,						-- �����ն�;�� (0��PC  1��Android  2��IOS  3��Web)
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
); 







