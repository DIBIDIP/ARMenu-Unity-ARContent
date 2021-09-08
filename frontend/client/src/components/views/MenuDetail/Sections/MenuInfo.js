import React from 'react'
import { Descriptions, Badge } from 'antd';

function MenuInfo(props) {

    let { menu } = props;

    return (
        <Descriptions title="[메뉴정보]" bordered>
            <Descriptions.Item label="[메뉴 이름]">Menu Name</Descriptions.Item>
            {/* <Descriptions.Item label="[메뉴 이름]">{movie.original_title}</Descriptions.Item> */}
            <Descriptions.Item label="[출시일]">Release Day</Descriptions.Item>
            {/* <Descriptions.Item label="[출시일]">{movie.release_date}</Descriptions.Item> */}
            <Descriptions.Item label="[조리 시간]">Cooking Time</Descriptions.Item>
            {/* <Descriptions.Item label="[조리 시간]">{movie.runtime}</Descriptions.Item> */}
            {/* <Descriptions.Item label="vote_average" span={2}>
                {movie.vote_average}
            </Descriptions.Item> */}
            {/* <Descriptions.Item label="vote_count">{movie.vote_count}</Descriptions.Item> */}
            <Descriptions.Item label="[가게 이름]">Restaurant Name</Descriptions.Item>
            {/* <Descriptions.Item label="[가게 이름]">{movie.status}</Descriptions.Item> */}
            <Descriptions.Item label="[알러지 정보]">Allergy Informations</Descriptions.Item>
            {/* <Descriptions.Item label="[알러지 정보]">{movie.popularity}</Descriptions.Item> */}
        </Descriptions>
    )
}

export default MenuInfo