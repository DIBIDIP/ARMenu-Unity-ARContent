import React from 'react';
import { Menu } from 'antd';
const SubMenu = Menu.SubMenu;
const MenuItemGroup = Menu.ItemGroup;

function LeftMenu(props) {
  return (
    <Menu mode={props.mode}>
      <Menu.Item key="mail">
        <a href="/">Home(Main)</a>
      </Menu.Item>
      <Menu.Item key="favorite">
        <a href="/favorite">Favorited</a>
      </Menu.Item>
    </Menu>
  )
}

export default LeftMenu