import React from 'react';
import './App.css';
import Menu from './components/Menu';
import {BrowserRouter as Router, Switch, Route} from 'react-router-dom';
import { Routes } from './routes/Routes';
import {RouteWithSubRoutes} from './routes/Utils';
import Home from './pages/Home';
import NotFound from './pages/NotFound';

function App() {
  return (
    <div className="App">
      <Router>
        <Menu></Menu>
        <Switch>
          <Route exact path="/" component={Home}/>
          {Routes.map((route, i) => (
            <RouteWithSubRoutes key={i} {...route} />
          ))}
          <Route path="*" component={NotFound} />
        </Switch>
      </Router>
    </div>
  );
}

export default App;
