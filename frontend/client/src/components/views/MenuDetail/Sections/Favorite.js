import React, { useEffect, useState } from 'react'
import Axios from 'axios'
import { Button } from 'antd';

function Favorite(props) {

    const menuId = props.menuId
    const userFrom = props.userFrom
    const menuTitle = props.menuInfo.title
    const menuPost = props.menuInfo.backdrop_path
    const menuRunTime = props.menuInfo.runtime


    const [FavoriteNumber, setFavoriteNumber] = useState(0)
    const [Favorited, setFavorited] = useState(false)
    let variables = {
        userFrom: userFrom,
        menuId: menuId,
        menuTitle: menuTitle,
        menuPost: menuPost,
        menuRunTime: menuRunTime
    }


    useEffect(() => {


        Axios.post('/api/favorite/favoriteNumber', variables)//정보가 MongoDB에 들어가있는 favorite 정보들을 꺼내온다 
            .then(response => {
                setFavoriteNumber(response.data.favoriteNumber)//FavoriteNumber 에 대한 정보 삽입
                if (response.data.success) {
                } else {
                    alert('숫자 정보를 가져오는데 실패 했습니다.')
                }
            })


        Axios.post('/api/favorite/favorited', variables)
            .then(response => {
                if (response.data.success) {
                    setFavorited(response.data.favorited)//Favorited 했는지 삽입 --> 그래서 useState 에는 기존에 false를 줌
                } else {
                    alert('정보를 가져오는데 실패 했습니다.')
                }
            })



    }, [])


    const onClickFavorite = () => {

        if (Favorited) {
            Axios.post('/api/favorite/removeFromFavorite', variables)
                .then(response => {
                    if (response.data.success) {
                        setFavoriteNumber(FavoriteNumber - 1)
                        setFavorited(!Favorited)
                    } else {
                        alert('Favorite 리스트에서 지우는 걸 실패했습니다.')
                    }
                })


        } else {
            Axios.post('/api/favorite/addToFavorite', variables)
                .then(response => {
                    if (response.data.success) {
                        setFavoriteNumber(FavoriteNumber + 1)
                        setFavorited(!Favorited)

                    } else {
                        alert('Favorite 리스트에서 추가하는 걸 실패했습니다.')
                    }
                })
        }

    }



    return (
        <div>
            <Button onClick={onClickFavorite}>{Favorited ? " 즐겨찾기 해제" : "즐겨찾기 추가 "}  {FavoriteNumber}  </Button>

        </div>
    )
}

export default Favorite