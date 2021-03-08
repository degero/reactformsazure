import React from 'react';
import {
    Route,
} from "react-router-dom";

function RouteWithSubRoutes(route: any) {
    console.log('setting up route' + route.route);
        return (
            <Route path={route.route}
            render={ props => (
            // pass the sub-routes down to keep nesting
            <route.component { ...props } routes = { route.routes } />
            )}
            />
);
}

export { RouteWithSubRoutes }