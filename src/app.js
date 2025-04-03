import React from 'react';
import { Route, Routes, Link } from 'react-router-dom';
import SinhvienList from './components/SinhVien/SinhvienList';
import SinhvienForm from './components/SinhVien/SinhvienForm';
import SinhvienDetail from './components/SinhVien/SinhvienDetail';
import BangDiemList from './components/BangDiem/BangDiemList';
import BangDiemDetail from './components/BangDiem/BangDiemDetail';
import './styles.css';

function App() {
  return (
    <div className="app">
      <nav>
        <Link to="/app/bangdiem">Grade List</Link> |{' '}
        <Link to="/app/bangdiem">Grade List</Link> |{' '}
        <Link to="/app/sinhvien">Student List</Link> |{' '}
        <Link to="/app/add">Add New Student</Link>
      </nav>
      <Routes>
        <Route path="/bangdiem" element={<BangDiemList />} />
        <Route path="/sinhvien" element={<SinhvienList />} />
        <Route path="/add" element={<SinhvienForm />} />
        <Route path="/edit/:id" element={<SinhvienForm />} />
        <Route path="/detail/:id" element={<SinhvienDetail />} />
        <Route path="/bangdiem/:msv" element={<BangDiemDetail />} />
      </Routes>
    </div>
  );
}

export default App;