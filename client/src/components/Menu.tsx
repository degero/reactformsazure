import React from 'react';
import {Link} from 'react-router-dom';
function Menu() {
    return(<div>Menu1 <Link to="/">Home</Link>
    <Link to="/form">Form</Link>
    <Link to="/list">Users</Link>
    </div>
    );
}

export default Menu;