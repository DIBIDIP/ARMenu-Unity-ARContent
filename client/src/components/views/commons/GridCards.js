import React from 'react'
import { Col } from 'antd';//css Framework 를 이용하겠다


function GridCards(props) {
    if (props.landingPage) {//LandingPage
        return (
            <Col lg={6} md={8} xs={24}>{/* 총 크기가 24인데, 전체화면일때는 6으로 즉 4칸, 줄어들면 8으로 즉 3칸, 제일작을때는 24 즉 한칸만 */}
                <div style={{ position: 'relative' }}>
                    <a href={`/menu/${props.menuId}`} >
                        <h2>Registered Menus</h2>
                        {/* <img style={{ width: '100%', height: '320px' }} src={props.image} alt={props.menuName} /> */}
                    </a>
                </div>
            </Col>
        )
    } 
}

export default GridCards