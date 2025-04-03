import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';

const BangDiemList = () => {
    const [bangDiems, setBangDiems] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchBangDiems();
    }, []);

    const fetchBangDiems = async () => {
        try {
            const response = await axios.get(`${process.env.REACT_APP_API_BASE_URL}/bangdiem`);
            setBangDiems(response.data);
        } catch (err) {
            console.error('Error fetching grades:', err); // Log error for debugging
            setError('Không thể tải danh sách bảng điểm');
        }
    };

    return (
        <div>
            <h2>Danh sách bảng điểm</h2>
            {error && <p className="error">{error}</p>}
            <table>
                <thead>
                    <tr>
                        <th>Mã SV</th>
                        <th>Điểm Học kỳ 1 Lần 1</th>
                        <th>Điểm Học kỳ 1 Lần 2</th>
                        <th>Điểm Học kỳ 1 Lần 3</th>
                        <th>Điểm Học kỳ 2 Lần 1</th>
                        <th>Điểm Học kỳ 2 Lần 2</th>
                        <th>Điểm Học kỳ 2 Lần 3</th>
                        <th>Điểm Cuối kỳ</th>
                        <th>Điểm Trung bình</th>
                        <th>Đạt</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    {bangDiems.map((bd) => (
                        <tr key={bd.msv}>
                            <td>{bd.msv}</td>
                            <td>{bd.diemh1l1}</td>
                            <td>{bd.diemh1l2}</td>
                            <td>{bd.diemh1l3}</td>
                            <td>{bd.diemh2l1}</td>
                            <td>{bd.diemh2l2}</td>
                            <td>{bd.diemh2l3}</td>
                            <td>{bd.diemck}</td>
                            <td>{bd.diemtb}</td>
                            <td>{bd.dat}</td>
                            <td>
                                <Link to={`/bangdiem/${bd.msv}`}>Xem</Link>
                                <Link to={`/bangdiem/${bd.msv}`}>Sửa</Link>
                                <Link to={`/bangdiem/${bd.msv}`}>Xóa</Link>
                            </td>

                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default BangDiemList;
