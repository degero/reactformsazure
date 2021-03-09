
import React, { useEffect, useState } from 'react';
import { get } from '../utils/Http';
import { ApiPath, UrlPathMap } from '../utils/Settings';

// Return list of users
const List = () => {
    const [items, setItems ] = useState(null);

    useEffect(() => {
        if (!items) {
            get(ApiPath + UrlPathMap['user']).then((resp) => resp.json()).then((items) => {
                console.debug('items');
                setItems(items);
            }).catch((error) => {
                alert("Error loading items: " + error);
          });

        }
    });

    return (
        <div>Users
            <ul>
                { items ? ( items.map((item:any) => { return (
                    <li key={item.name}>{item.name} - {item.email}</li>
                );
                })
                ) : (
                    <></>
                )
            }
            </ul>
        </div>
    );
};

export default List;