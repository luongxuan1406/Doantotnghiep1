import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import Header from '../Layout/Header';
import Home from '../Layout/Home';

const SinhvienList = () => {
    console.log('SinhvienList rendered');
    const [sinhviens, setSinhviens] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchSinhviens();
    }, []);

    const fetchSinhviens = async () => {
        try {
            const response = await axios.get(`${process.env.REACT_APP_API_BASE_URL}/sinhvien`);
            setSinhviens(response.data);
        } catch (err) {
            console.error('Error fetching students:', err); // Log error for debugging
            setError('Không thể tải danh sách sinh viên');
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm('Bạn có chắc muốn xóa sinh viên này?')) {
            try {
                await axios.delete(`${process.env.REACT_APP_API_BASE_URL}/sinhvien/${id}`);
                fetchSinhviens();
            } catch (err) {
                console.error('Error deleting student:', err); // Log error for debugging
                setError('Không thể xóa sinh viên');
            }
        }
    };

    return (
        <div>


            <h2>Danh sách sinh viên</h2>
            <Link to="/bangdiem">Xem danh sách bảng điểm</Link>
            {error && <p className="error">{error}</p>}
            <table>
                <thead>
                    <tr>
                        <th>Mã SV</th>
                        <th>Họ tên</th>
                        <th>Mã khoa</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    {sinhviens.map((sv) => (
                        <tr key={sv.msv}>
                            <td>{sv.msv}</td>
                            <td>{sv.hoten}</td>
                            <td>{sv.makhoa}</td>
                            <td>
                                <Link to={`/detail/${sv.msv}`}>Xem</Link> |{' '}
                                <Link to={`/edit/${sv.msv}`}>Sửa</Link> |{' '}
                                <button onClick={() => handleDelete(sv.msv)}>Xóa</button> |{' '}
                                <Link to={`/bangdiem/${sv.msv}`}>Bảng điểm</Link>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>


        </div>
    );
};

export default SinhvienList;