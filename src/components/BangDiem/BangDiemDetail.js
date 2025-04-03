import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';

const BangDiemDetail = () => {
    const { msv } = useParams();
    const navigate = useNavigate();
    const [bangDiem, setBangDiem] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchBangDiem();
    }, [msv]);

    const fetchBangDiem = async () => {
        try {
            const response = await axios.get(`${process.env.REACT_APP_API_BASE_URL}/bangdiem/${msv}`);
            setBangDiem(response.data);
        } catch (err) {
            console.error('Error fetching grade details:', err); // Log error for debugging
            setError('Không thể tải thông tin bảng điểm');
        }
    };

    if (!bangDiem) return <p>Đang tải...</p>;

    return (
        <div>
            <h2>Chi tiết bảng điểm</h2>
            {error && <p className="error">{error}</p>}
            <p><strong>Mã sinh viên:</strong> {bangDiem.msv}</p>
            <p><strong>Điểm Học kỳ 1 Lần 1:</strong> {bangDiem.diemh1l1}</p>
            <p><strong>Điểm Học kỳ 1 Lần 2:</strong> {bangDiem.diemh1l2}</p>
            <p><strong>Điểm Học kỳ 1 Lần 3:</strong> {bangDiem.diemh1l3}</p>
            <p><strong>Điểm Học kỳ 2 Lần 1:</strong> {bangDiem.diemh2l1}</p>
            <p><strong>Điểm Học kỳ 2 Lần 2:</strong> {bangDiem.diemh2l2}</p>
            <p><strong>Điểm Học kỳ 2 Lần 3:</strong> {bangDiem.diemh2l3}</p>
            <p><strong>Điểm Cuối kỳ:</strong> {bangDiem.diemck}</p>
            <p><strong>Điểm Trung bình:</strong> {bangDiem.diemtb}</p>
            <p><strong>Đạt:</strong> {bangDiem.dat}</p>
            <button onClick={() => navigate('/bangdiem')}>Quay lại</button>
        </div>
    );
};

export default BangDiemDetail;
