import React, { Component } from 'react';
import Odd from './OddComponent';

class Bet extends Component {

    constructor(props) {
        super(props);
        this.ws = props.ws;
        this.state = {
            id: props.data.id,
            name: props.data.name,
            odds: props.data.odds || []
        };
    }

    render() {
        return (
            <li className='list-group-item'>
                <span>{this.state.name}</span>
                <ul className='list-group-horizontal'>
                    {this.state.odds.map((odd, i)  => (
                        <Odd key={i} data={odd} ws={this.ws}></Odd>
                    ))}
                </ul>
            </li>
        );
    }
}

export default Bet;
