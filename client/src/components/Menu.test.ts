import Menu from './Menu';
import { render, screen } from '@testing-library/react';

describe('Menu component', () => {
    it('should render text "menu"', () => {
        render(<Menu />);
        const linkElement = screen.getByText(/menu/i);
        expect(linkElement).toBeInTheDocument();
    })
});