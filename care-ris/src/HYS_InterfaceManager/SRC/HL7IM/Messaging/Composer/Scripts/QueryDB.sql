use XDSDataDB

select * from t_msg_box_cda_content order by data_dt
select * from t_msg_box_cda_keyword order by data_dt
select * from t_msg_box_cda_reference order by data_dt
select * from t_msg_box_cda_status order by data_dt
select * from t_msg_box_cda_status order by entity_id

select * from t_msg_box_cda_content T
join t_msg_box_cda_status S on T.data_id = S.data_id
order by T.data_dt

select * from t_msg_box_cda_content T
join t_msg_box_cda_keyword K on T.data_id = K.data_id
order by T.data_dt
