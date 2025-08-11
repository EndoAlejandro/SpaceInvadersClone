namespace SpaceInvaders.Input
{
    public class InputReader
    {
        private readonly MainControls _controls = new MainControls();

        public bool Shoot => _controls != null && _controls.Player.Shoot.IsPressed();
        
        public float Movement => _controls != null ? _controls.Player.Move.ReadValue<float>() : 0f;
        
        public void EnableMainInput() => _controls.Player.Enable();

        public void DisableMainInput() => _controls.Player.Disable();
    }
}