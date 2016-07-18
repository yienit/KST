----------------------------
-- 名称： 初始化脚本(MySQL)
-- 作者： 曹江波
-- 时间： 2016.01.23
----------------------------


-- 初始化预定义考试科目及预定义章节表数据
INSERT INTO SysCourse(ID, Name, Code, Duration, Description) VALUES (1, '会计基础','kj_kjjc', 60, '会计从业资格考试科目');
INSERT INTO SysCourse(ID, Name, Code, Duration, Description) VALUES (2, '财经法规与职业道德','kj_cjfg&zydd', 60, '会计从业资格考试科目');
INSERT INTO SysCourse(ID, Name, Code, Duration, Description) VALUES (3, '会计电算化','kj_kjdsh', 60, '会计从业资格考试科目');
INSERT INTO SysCourse(ID, Name, Code, Duration, Description) VALUES (4, '经济法基础','kj_jjfjc', 90, '初级会计职称考试考试科目');
INSERT INTO SysCourse(ID, Name, Code, Duration, Description) VALUES (5, '初级会计实务','kj_cjkjsw', 120, '初级会计职称考试考试科目');

-- 初始化预定义章节

-- 会计基础章节
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (1,1,1,'总论');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (2,1,2,'会计要素与会计科目');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (3,1,3,'会计等式与复式记账');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (4,1,4,'会计凭证');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (5,1,5,'会计账簿');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (6,1,6,'账务处理程序');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (7,1,7,'财产清查');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (8,1,8,'财务会计报告');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (9,1,9,'会计档案');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (10,1,10,'主要经济业务事项账务处理');

-- 财经法规与职业道德章节
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (11,2,1,'会计法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (12,2,2,'支付结算法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (13,2,3,'税收法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (14,2,4,'财政法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (15,2,5,'会计账簿');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (16,2,6,'会计职业道德');

-- 会计电算化章节
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (17,3,1,'会计电算化概述');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (18,3,2,'会计软件的运行环境');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (19,3,3,'会计软件的应用');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (20,3,4,'电子表格软件在会计中的应用');

-- 经济法基础章节
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (21,4,1,'总论');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (22,4,2,'劳动合同与社会保险法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (23,4,3,'支付结算法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (24,4,4,'增值税、消费税、营业税法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (25,4,5,'企业所得税、个人所得税法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (26,4,6,'其他税收法律制度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (27,4,7,'税收征收管理法律制度');

-- 初级会计实务章节
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (28,5,1,'资产');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (29,5,2,'负债');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (30,5,3,'所有者权益');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (31,5,4,'收入');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (32,5,5,'费用');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (33,5,6,'利润');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (34,5,7,'财务报告度');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (35,5,8,'产品成本核算');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (36,5,9,'产品成本计算与分析');
INSERT INTO SysChapter(ID, CourseID, ChapterIndex, Name) VALUES (37,5,10,'事业单位会计基础');