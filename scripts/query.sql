SELECT this_.Id           as id1_3_1_,
       this_.Title        as title2_3_1_,
       this_.Description  as description3_3_1_,
       this_.ISBN         as isbn4_3_1_,
       this_.Price        as price5_3_1_,
       this_.AuthorId     as authorid6_3_1_,
       this_.CategoryId   as categoryid7_3_1_,
       author1_.Id        as id1_0_0_,
       author1_.FirstName as firstname2_0_0_,
       author1_.LastName  as lastname3_0_0_
FROM Books this_
         inner join Authors author1_ on this_.AuthorId = author1_.Id
WHERE this_.Title like @p0
  and author1_.FirstName like @p1;
@p0= '%Avengers%', @p1 = '%Steve%'

exec sp_executesql("select * from Author where id = @p0", p0 = 123)