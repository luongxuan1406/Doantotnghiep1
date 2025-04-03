import React from 'react';
import { Bar, Pie } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
    ArcElement,
} from 'chart.js';

// Đăng ký các thành phần cần thiết cho ChartJS
ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
    ArcElement
);

const Home = () => {
    // Dữ liệu mẫu cho biểu đồ kết quả học tập (Bar Chart)
    const chartData = {
        labels: ['Hệ số 1', 'Hệ số 1', 'Hệ số 2', 'Hệ số 3'],
        datasets: [
            {
                label: 'Điểm trung bình',
                data: [8.5, 7.8, 8.2, 9.0],
                backgroundColor: 'rgba(242, 73, 0, 0.6)',
                borderColor: 'rgb(240, 76, 0)',
                borderWidth: 1,
            },
        ],
    };

    // Cấu hình biểu đồ kết quả học tập
    const chartOptions = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Kết quả học tập năm 2024 - 2025',
            },
        },
        scales: {
            y: {
                beginAtZero: true,
                max: 10,
                title: {
                    display: true,
                    text: 'Điểm số'
                }
            },
            x: {
                title: {
                    display: true,
                    text: 'Học kỳ'
                }
            }
        }
    };

    // Dữ liệu cho biểu đồ tiến độ học tập (Pie Chart)
    const progressData = {
        labels: ['Đã hoàn thành', 'Còn lại'],
        datasets: [
            {
                label: 'Tiến độ học tập',
                data: [179, 15],
                backgroundColor: [
                    'rgba(54, 162, 235, 0.6)',
                    'rgba(255, 99, 132, 0.6)',
                ],
                borderColor: [
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 99, 132, 1)',
                ],
                borderWidth: 1,
            },
        ],
    };

    // Cấu hình biểu đồ tiến độ học tập
    const progressOptions = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Tiến độ học tập',
            },
            tooltip: {
                callbacks: {
                    label: function (context) {
                        const label = context.label || '';
                        const value = context.raw || 0;
                        return `${label}: ${value} tín chỉ`;
                    }
                }
            }
        }
    };

    // Dữ liệu cho widgets
    const widgetData = [
        { icon: '/register.png', text: 'Đăng kí thực tập' },
        { icon: '/business.png', text: 'Quản lý doanh nghiệp' },
        { icon: '/teacher.png', text: 'Quản lý giảng viên' },
        { icon: '/student.png', text: 'Quản lý sinh viên' },
        { icon: '/class.png', text: 'Quản lý lớp học phần' },
        { icon: '/report.png', text: 'Báo cáo thực tập' },
        { icon: '/faculty.png', text: 'Quản lý Khoa' },
        { icon: '/unlock.png', text: 'Quản lý tài khoản' },
    ];

    // Dữ liệu cho menu sidebar
    const sidebarItems = [
        { icon: '/home.png', text: 'Trang chủ' },
        { icon: '/register.png', text: 'Đăng ký thực tập' },
        { icon: '/results.png', text: 'Kết quả học tập' },
        { icon: '/progress.png', text: 'Tiến độ học tập' },
        { icon: '/credits.png', text: 'Số tín chỉ' },
        { icon: '/logout.png', text: 'Đăng xuất' },
    ];

    return (
        <div className="app-container">
            {/* Sidebar */}
            <div className="sidebar">
                <div className="sidebar-header">
                    <img src="/logo.jpg" alt="Logo" className="sidebar-logo" />
                    <span>Quản lý thực tập</span>
                </div>
                <ul className="sidebar-menu">
                    {sidebarItems.map((item, index) => (
                        <li key={index} className="sidebar-item">
                            <img src={item.icon} alt={item.text} className="sidebar-icon" />
                            <span>{item.text}</span>
                        </li>
                    ))}
                </ul>
            </div>

            {/* Nội dung chính */}
            <div className="main-content">
                <div className="dashboard">
                    {/* Thông tin sinh viên */}
                    <div className="student-info">
                        <h2>Thông tin sinh viên</h2>
                        <div className="student-details">
                            <img src="/us.png" alt="Avatar" className="avatar" />
                            <div className="details">
                                <p><strong>MSSV:</strong> 21103101170</p>
                                <p><strong>Họ tên:</strong> NGUYỄN XUÂN LƯƠNG</p>
                                <p><strong>Giới tính:</strong> Nam</p>
                                <p><strong>Ngày sinh:</strong> 16/10/2003</p>
                                <p><strong>Nơi sinh:</strong> Thái Bình</p>
                                <p><strong>Ngành:</strong> Công nghệ thông tin</p>
                                <button>Xem chi tiết</button>
                            </div>
                        </div>
                    </div>

                    {/* Nhắc nhở, chương trình */}
                    <div className="reminders">
                        <h3>Nhắc nhở, chương trình</h3>
                        <div className="reminder-card">
                            <p>Trường Đại học Kinh tế Kỹ thuật Công nghiệp</p>
                            <p>Thông báo đăng kí doanh nghiệp thực tập từ ngày 16/12/2024</p>
                            <p>Thông báo trả kết quả và kết thúc thực tập 19/1/2025</p>
                            <button>Xem chi tiết</button>
                        </div>
                        <div className="reminder-stats">
                            <div className="stat">
                                <p>Tiến độ thực tập</p>
                                <button>Xem chi tiết</button>
                            </div>
                            <div className="stat">
                                <p>Kết quả thực tập</p>
                                <button>Xem chi tiết</button>
                            </div>
                        </div>
                    </div>

                    {/* Các widget nhỏ */}
                    <div className="widgets">
                        {widgetData.map((widget, index) => (
                            <div className="widget" key={index}>
                                <img src={widget.icon} alt={widget.text} className="widget-icon" />
                                <span>{widget.text}</span>
                            </div>
                        ))}
                    </div>

                    {/* Kết quả học tập */}
                    <div className="results">
                        <h3>Kết quả học tập</h3>
                        <select>
                            <option>2024 - 2025</option>
                        </select>
                        <div className="chart" style={{ height: '300px' }}>
                            <Bar data={chartData} options={chartOptions} />
                        </div>
                    </div>

                    {/* Tiến độ học tập */}
                    <div className="progress">
                        <h3>Tiến độ học tập</h3>
                        <select>
                            <option>Lớp học phần</option>
                        </select>
                        <div className="progress-circle" style={{ height: '300px' }}>
                            <Pie data={progressData} options={progressOptions} />
                        </div>
                    </div>

                    {/* Số tín chỉ */}
                    <div className="credits">
                        <h3>Số tín chỉ</h3>
                        <ul>
                            <li>Thực tập cuối khóa ngành CNTT: 5</li>
                            <li>Lịch sử Đảng cộng sản Việt Nam: 2</li>
                            <li>Triết học Mác - Lênin: 3</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Home;