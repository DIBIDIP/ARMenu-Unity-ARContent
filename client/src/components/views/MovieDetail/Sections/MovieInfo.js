import React from 'react'
import { Descriptions, Badge } from 'antd';

function MovieInfo(props) {

    let { movie } = props;

    return (
        <Descriptions title="Menu Info(메뉴 정보)" bordered>
            <Descriptions.Item label="Menu name(메뉴이름)">{movie.original_title}</Descriptions.Item>
            <Descriptions.Item label="release date(출시일)">{movie.release_date}</Descriptions.Item>
            {/* <Descriptions.Item label="revenue">{movie.revenue}</Descriptions.Item> */}
            <Descriptions.Item label="CookingTime(조리시간)">{movie.runtime}</Descriptions.Item>
            {/* <Descriptions.Item label="vote_average" span={2}>
                {movie.vote_average}
            </Descriptions.Item> */}
            {/* <Descriptions.Item label="vote_count">{movie.vote_count}</Descriptions.Item> */}
            <Descriptions.Item label="출시상태">{movie.status}</Descriptions.Item>
            <Descriptions.Item label="인기도">{movie.popularity}</Descriptions.Item>
        </Descriptions>
    )
}

export default MovieInfo