// src/index.js
import React from 'react';
import ReactDOM from 'react-dom/client';
import './styles.css';
import Header from './components/Layout/Header';
import Home from './components/Layout/Home';
import Footer from './components/Layout/Footer';

const App = () => {
    return (
        <div className="app">
            <Header />
            <main>
                <Home />
            </main>
            <Footer />
        </div>
    );
};

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);