import React, { useEffect, useState } from 'react'
import { List, Avatar, Row, Col, Button } from 'antd';
import axios from 'axios';

import Comments from './Sections/Comments'
import LikeDislikes from './Sections/LikeDislikes';
import { API_URL, API_KEY, IMAGE_BASE_URL, IMAGE_SIZE } from '../../Config'
import GridCards from '../commons/GridCards';
import MainImage from '../LandingPage/Sections/MainImage';
import MenuInfo from './Sections/MenuInfo';
import Favorite from './Sections/Favorite';

function MenuDetailPage(props) {
    const menuId = props.match.params.menuId
    const [Menu, setMenu] = useState([])
    const [Casts, setCasts] = useState([])
    const [CommentLists, setCommentLists] = useState([])
    const [LoadingForMenu, setLoadingForMenu] = useState(true)
    const [LoadingForCasts, setLoadingForCasts] = useState(true)
    const [ActorToggle, setActorToggle] = useState(false)
    const menuVariable = {
        menuId: menuId
    }

    useEffect(() => {
        let endpointForMenuInfo = `${API_URL}movie/${menuId}?api_key=${API_KEY}&language=en-US`;//해당 부분 API_URL/API_KEY(많이 사용하여 config.js 파일에 선언해둠) 과 movie/popular? 등 그외의 소스를 규리가 작성해놓은 파일의 주소로 변환해서 받아와야하는데 어떻게 해야할지를 모르겠습니다 ㅠㅠ 
        fetchDetailInfo(endpointForMenuInfo)
        axios.post('/api/comment/getComments', menuVariable)
            .then(response => {
                console.log(response)
                if (response.data.success) {
                    console.log('response.data.comments', response.data.comments)
                    setCommentLists(response.data.comments)
                } else {
                    alert('Failed to get comments Info')
                }
            })
    }, [])

    const toggleActorView = () => {
        setActorToggle(!ActorToggle)
    }

    const fetchDetailInfo = (endpoint) => {
        fetch(endpoint)
            .then(result => result.json())
            .then(result => {
                console.log(result)
                setMenu(result)
                setLoadingForMenu(false)
                let endpointForCasts = `${API_URL}movie/${menuId}/credits?api_key=${API_KEY}`;//해당 부분 API_URL/API_KEY(많이 사용하여 config.js 파일에 선언해둠) 과 movie/popular? 등 그외의 소스를 규리가 작성해놓은 파일의 주소로 변환해서 받아와야하는데 어떻게 해야할지를 모르겠습니다 ㅠㅠ 
                fetch(endpointForCasts)
                    .then(result => result.json())
                    .then(result => {
                        console.log(result)
                        setCasts(result.cast)
                    })
                setLoadingForCasts(false)
            })
            .catch(error => console.error('Error:', error)
            )
    }

    const updateComment = (newComment) => {
        setCommentLists(CommentLists.concat(newComment))
    }

    return (
        <div>
            {/* Header */}
            {!LoadingForMenu ?
                <MainImage
                    image={`${IMAGE_BASE_URL}${IMAGE_SIZE}${Menu.backdrop_path}`}//기존에 받아와서 사용하던 API 는 .backdrop_path 를 통해서 사진을 불러올 수 있었는데 새로 가져와야하는 api 쪽에서는 어떻게 가져오는지..
                    title={Menu.original_title}
                    text={Menu.overview}
                />
                :
                <div>loading...</div>
            }


            {/* Body */}
            <div style={{ width: '85%', margin: '1rem auto' }}>

                <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
                    <Favorite menuInfo={Menu} menuId={menuId} userFrom={localStorage.getItem('userId')} />
                </div>


                {/* Menu Info */}
                {!LoadingForMenu ?
                    <MenuInfo menu={Menu} />
                    :
                    <div>loading...</div>
                }

                <br />

                <div style={{ display: 'flex', justifyContent: 'center' }}>
                    <LikeDislikes video videoId={menuId} userId={localStorage.getItem('userId')} />
                </div>

                {/* Comments */}
                <Comments menuTitle={Menu.original_title} CommentLists={CommentLists} postId={menuId} refreshFunction={updateComment} />

            </div>

        </div>
    )
}

export default MenuDetailPage