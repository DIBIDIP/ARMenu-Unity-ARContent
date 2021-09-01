import React, { useEffect, useState } from 'react'
import { FaCode } from"react-icons/fa";
import { API_URL, API_KEY, IMAGE_BASE_URL } from '../../Config';
import MainImage from './Sections/MainImage';
import axios from 'axios';
import GridCards from '../commons/GridCards';
import { Row } from 'antd'; 

function LandingPage() {


    const [Movies, setMovies] = useState([])
    const [MainMovieImage, setMainMovieImage] = useState(null)
    const [CurrentPage, setCurrentPage] = useState(0)

    useEffect(() => {
        const endpoint = `${API_URL}movie/popular?api_key=${API_KEY}&language=en-US&page=1`;
        fetchMovies(endpoint)

    }, [])


    const fetchMovies = (endpoint) => {
        fetch(endpoint)
            .then(response => response.json())
            .then(response => {
                console.log(response)
                setMovies([...Movies, ...response.results])
                setMainMovieImage(response.results[0])
                setCurrentPage(response.page)
            })
    }

    const loadMoreItems = () => {

        const endpoint = `${API_URL}movie/popular?api_key=${API_KEY}&language=en-US&page=${CurrentPage + 1}`;
        fetchMovies(endpoint)

    }


    return (
        <div style={{ width: '100%', margin: '0' }}>

            {/* Main Image */}
            {MainMovieImage &&
                <MainImage
                    image={`${IMAGE_BASE_URL}w1280${MainMovieImage.backdrop_path}`}
                    title={MainMovieImage.original_title}
                    text={MainMovieImage.overview}
                />
            }


            <div style={{ width: '85%', margin: '1rem auto' }}>

                <h2>Latest Registered Menus (등록된 메뉴)</h2>
                <hr />

                {/* Movie Grid Cards */}

                <Row gutter={[16, 16]} >

                    {Movies && Movies.map((movie, index) => (
                        <React.Fragment key={index}>
                            <GridCards
                                landingPage
                                image={movie.poster_path ?
                                    `${IMAGE_BASE_URL}w500${movie.poster_path}` : null}
                                movieId={movie.id}
                                movieName={movie.original_title}
                            />
                        </React.Fragment>
                    ))}

                </Row>

            </div>

            <div style={{ display: 'flex', justifyContent: 'center' }}>
                <button onClick={loadMoreItems}> Load More(더보기)</button>
            </div>

        </div>
    )
}

export default LandingPage






//import { response } from 'express';
// import React,{useEffect,useState} from 'react';
// import { FaCode } from "react-icons/fa";
// import { API_URL, API_KEY, IMAGE_BASE_URL } from '../../Config';
// import MainImage from './Sections/MainImage';
// import GridCards from '../commons/GridCards';
// import { Row } from 'antd';//css FrameWork 사용



// function LandingPage() {

//     const [Movies, setMovies] = useState([])
//     const [MainMovieImage, setMainMovieImage] = useState(null)
//     const [CurrentPage, setCurrentPage] = useState(0)//처음에 들어오는 page 로 currentPage 설정, LoadMore 버튼 클릭마다 페이지가 추가

//     useEffect(() => {
//         const endpoint = `${API_URL}movie/popular?api_key=${API_KEY}&language=en-US&page=1`;
//         fetchMovies(endpoint)

//     }, [])


//     const fetchMovies = (endpoint) => {
//         fetch(endpoint)
//             .then(response => response.json())
//             .then(response => {
//                 console.log(response)
//                 setMovies([...Movies, ...response.results])//다음과 같이 해줌으로써 위에 선언한 const useState 부분의 Movies에 response.results가 들어간다 즉 영화에 대한 정보들이 들어감
//                 setMainMovieImage(response.results[0])
//                 setCurrentPage(response.page)//가장 유명한 첫 페이지 상단에 뜰 영화
//             })
//     }// 중복되는 코드 fetchMoview 로 간소화


//     const loadMoreItems = () => {
//         const endpoint = `${API_URL}movie/popular?api_key=${API_KEY}&language=en-US&page=${CurrentPage + 1}`;
//         fetchMovies(endpoint)
//     }

    
//     return (
//         <div style={{width: '100%', margin:'0'}}>
//             {/*Main Image*/}
//         {MainMovieImage && 
//             <MainImage 
//             image={`${IMAGE_BASE_URL}w1280${MainMovieImage.backdrop_path}`}
//             title={MainMovieImage.original_title}
//             text={MainMovieImage.overview}
//             />
//         }

//         {/* MainMovieImage&& 의 의미는 MainMovieImage가 있으면 &&뒤의 내용을 실행해라는 의미, 즉 정보를 가져오기전에 랜더링 되어서 오류나는것을 방지 */}
//         {/* 페이지 틀어서 console 찍어보면 해당 movie 의 이미지 파일 이름이 backdrop_path에 저장되어있음 */}
//         {/* image가 이제 해당 image 정보를 가지고 있는것이고, 이것을 MainImage.js 파일에 props을 이용하여 뿌려줘야한다 */}
            
//         <div style={{width:'85%',margin:'1rem auto'}}>
//             <h2>Movies by latest</h2>
//             <hr />
//             {/*Movie Grid Cards*/}
//                 <Row gutter={[16, 16]} >
//                     {Movies && Movies.map((movie, index) => (
//                         <React.Fragment key={index}>
//                             <GridCards
//                                 image={movie.poster_path ?
//                                     `${IMAGE_BASE_URL}w500${movie.poster_path}` : null}
//                                 movieId={movie.id}
//                                 movieName={movie.original_title}
//                             />
//                         </React.Fragment>
//                     ))}
//                 </Row>
//                 {/* 위에서 useState로 받아온 Moviee가 있다면, GridCards 들의 image 경로를 movie 목록들의 poster_path 주소를 가져와서 이미지를 넣어준다 
//                 , MovieId,MovieName 정보들은 페이지 console 까보면 나오는데 이들은 각각 grid 클릭시에 세부정보로 들어갈 수 있게 해줌
//                 Row gutter 는 grid 사이사이의 여백을 줄 수 있음
//                 */}
//         </div>

//         <div style={{display:'flex',justifyContent:'center'}}>
//             <button onClick={loadMoreItems}> Load More</button>
//         </div>

//         </div>
//     )
// }

// export default LandingPage
