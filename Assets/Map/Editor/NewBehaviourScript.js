class RenderCubemapWizard extends ScriptableWizard {
    var renderFromPosition : Transform;
    var cubemap : Cubemap;

    function OnWizardUpdate () {
        helpString = "Select transform to render from and cubemap to render into";
        isValid = (renderFromPosition != null) && (cubemap != null);
    }

    function OnWizardCreate () {
        // create temporary camera for rendering
        var go = new GameObject("CubemapCamera", Camera);
        // place it on the object
        go.transform.position = renderFromPosition.position;
        go.transform.rotation = Quaternion.identity;

        // render into cubemap
        go.GetComponent.<Camera>().RenderToCubemap(cubemap);

        // destroy temporary camera
        DestroyImmediate(go);
    }

    @MenuItem("Component/Render into Cubemap1")
    static function RenderCubemap () {
        ScriptableWizard.DisplayWizard.<RenderCubemapWizard>(
            "Render cubemap", "Render!");
    }
}