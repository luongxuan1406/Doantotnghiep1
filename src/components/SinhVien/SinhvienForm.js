import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';

const SinhvienForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [sinhvien, setSinhvien] = useState({
        msv: '',
        hoten: '',
        makhoa: '',
    });
    const [error, setError] = useState(null);

    useEffect(() => {
        if (id) {
            fetchSinhvien();
        }
    }, [id]);

    const fetchSinhvien = async () => {
        try {
            const response = await axios.get(`https://localhost:7289/api/sinhvien/${id}`);
            setSinhvien(response.data);
        } catch (err) {
            setError('Không thể tải thông tin sinh viên');
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setSinhvien({ ...sinhvien, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (id) {
                await axios.put(`https://localhost:7289/api/sinhvien/${id}`, sinhvien);
            } else {
                await axios.post('https://localhost:7289/api/sinhvien', sinhvien);
            }
            navigate('/');
        } catch (err) {
            setError(err.response?.data?.message || 'Có lỗi xảy ra');
        }
    };

    return (
        <div>
            <h2>{id ? 'Sửa sinh viên' : 'Thêm sinh viên'}</h2>
            {error && <p className="error">{error}</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Mã sinh viên:</label>
                    <input
                        type="text"
                        name="msv"
                        value={sinhvien.msv}
                        onChange={handleChange}
                        disabled={!!id}
                        required
                    />
                </div>
                <div>
                    <label>Họ tên:</label>
                    <input
                        type="text"
                        name="hoten"
                        value={sinhvien.hoten}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Mã khoa:</label>
                    <input
                        type="text"
                        name="makhoa"
                        value={sinhvien.makhoa}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit">{id ? 'Cập nhật' : 'Thêm'}</button>
                <button type="button" onClick={() => navigate('/')}>
                    Hủy
                </button>
            </form>
        </div>
    );
};

export default SinhvienForm;