import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';

const SinhvienDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [sinhvien, setSinhvien] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchSinhvien();
    }, [id]);

    const fetchSinhvien = async () => {
        try {
            const response = await axios.get(`https://localhost:7289/api/sinhvien/${id}`);
            setSinhvien(response.data);
        } catch (err) {
            setError('Không thể tải thông tin sinh viên');
        }
    };

    if (!sinhvien) return <p>Đang tải...</p>;

    return (
        <div>
            <h2>Chi tiết sinh viên</h2>
            {error && <p className="error">{error}</p>}
            <p><strong>Mã sinh viên:</strong> {sinhvien.msv}</p>
            <p><strong>Họ tên:</strong> {sinhvien.hoten}</p>
            <p><strong>Mã khoa:</strong> {sinhvien.makhoa}</p>
            <button onClick={() => navigate('/')}>Quay lại</button>
        </div>
    );
};

export default SinhvienDetail;