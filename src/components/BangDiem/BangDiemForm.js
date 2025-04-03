import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';

// filepath: d:/tnfrontend/src/components/BangDiem/BangDiemForm.js

const BangDiemForm = () => {
    const { msv } = useParams();
    const navigate = useNavigate();
    const [bangDiem, setBangDiem] = useState({
        msv: '',
        diemh1l1: '',
        diemh1l2: '',
        diemh1l3: '',
        diemh2l1: '',
        diemh2l2: '',
        diemh2l3: '',
        diemck: '',
        diemtb: '',
        dat: false,
    });
    const [error, setError] = useState(null);

    useEffect(() => {
        if (msv) {
            fetchBangDiem();
        }
    }, [msv]);

    const fetchBangDiem = async () => {
        try {
            const response = await axios.get(`${process.env.REACT_APP_API_BASE_URL}/bangdiem/${msv}`);
            setBangDiem(response.data);
        } catch (err) {
            setError('Không thể tải thông tin bảng điểm');
        }
    };

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setBangDiem({
            ...bangDiem,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (msv) {
                await axios.put(`${process.env.REACT_APP_API_BASE_URL}/bangdiem/${msv}`, bangDiem);
            } else {
                await axios.post(`${process.env.REACT_APP_API_BASE_URL}/bangdiem`, bangDiem);
            }
            navigate('/app/bangdiem');
        } catch (err) {
            setError(err.response?.data?.message || 'Có lỗi xảy ra');
        }
    };

    const handleDelete = async () => {
        if (window.confirm('Bạn có chắc muốn xóa bảng điểm này?')) {
            try {
                await axios.delete(`${process.env.REACT_APP_API_BASE_URL}/bangdiem/${msv}`);
                navigate('/app/bangdiem');
            } catch (err) {
                setError('Không thể xóa bảng điểm');
            }
        }
    };

    return (
        <div>
            <h2>{msv ? 'Sửa bảng điểm' : 'Thêm bảng điểm'}</h2>
            {error && <p className="error">{error}</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Mã sinh viên:</label>
                    <input
                        type="text"
                        name="msv"
                        value={bangDiem.msv}
                        onChange={handleChange}
                        disabled={!!msv}
                        required
                    />
                </div>
                <div>
                    <label>Điểm Học kỳ 1 Lần 1:</label>
                    <input
                        type="number"
                        name="diemh1l1"
                        value={bangDiem.diemh1l1}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Điểm Học kỳ 1 Lần 2:</label>
                    <input
                        type="number"
                        name="diemh1l2"
                        value={bangDiem.diemh1l2}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Điểm Học kỳ 1 Lần 3:</label>
                    <input
                        type="number"
                        name="diemh1l3"
                        value={bangDiem.diemh1l3}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Điểm Học kỳ 2 Lần 1:</label>
                    <input
                        type="number"
                        name="diemh2l1"
                        value={bangDiem.diemh2l1}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Điểm Học kỳ 2 Lần 2:</label>
                    <input
                        type="number"
                        name="diemh2l2"
                        value={bangDiem.diemh2l2}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Điểm Học kỳ 2 Lần 3:</label>
                    <input
                        type="number"
                        name="diemh2l3"
                        value={bangDiem.diemh2l3}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Điểm Cuối kỳ:</label>
                    <input
                        type="number"
                        name="diemck"
                        value={bangDiem.diemck}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Điểm Trung bình:</label>
                    <input
                        type="number"
                        name="diemtb"
                        value={bangDiem.diemtb}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Đạt:</label>
                    <input
                        type="checkbox"
                        name="dat"
                        checked={bangDiem.dat}
                        onChange={handleChange}
                    />
                </div>
                <button type="submit">{msv ? 'Cập nhật' : 'Thêm'}</button>
                {msv && (
                    <button type="button" onClick={handleDelete}>
                        Xóa
                    </button>
                )}
                <button type="button" onClick={() => navigate('/app/bangdiem')}>
                    Hủy
                </button>
            </form>
        </div>
    );
};

export default BangDiemForm;