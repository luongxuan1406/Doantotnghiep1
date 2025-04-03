// src/components/Layout/Header.js
import React from 'react';

const Header = () => {
    return (
        <header className="header">
            <div className="logo">
                <img src="/logoo.png" alt="Logo" />
                <span>PHẦN MỀM QUẢN LÝ THỰC TẬP CHO SINH VIÊN</span>
            </div>
            <div className="search-bar">
                <input type="text" placeholder="Tìm kiếm..." />
                <button className="search-button">Tìm kiếm</button>
            </div>
            <div className="user-info">
                <span>NGUYỄN XUÂN LƯƠNG</span>
                <img src="/user.png" alt="User" className="user-icon" />
            </div>
        </header>
    );
};

export default Header;
