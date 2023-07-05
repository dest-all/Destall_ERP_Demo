const view = {
    pushHistory: url => {
        history.pushState(null, null, url);
    },

    countItemsInsideElement: elemId => {
        const elem = document.getElementById(elemId);

        const result = elem.childNodes.length;
        return result;
    },

    subscribeForResize: async (containerId, dotnetCallback) => {
        const elem = document.getElementById(containerId);

        if (!elem) {
            return false;
        }

        const callback = async () => {
            const height = elem?.offsetHeight || 0;
            const width = elem?.offsetWidth || 0;

            await dotnetCallback.invokeMethodAsync(
                'Call',
                height || 0,
                width || 0
            );
        };

        await callback();

        const resizeObserver = new ResizeObserver(async () => {
            await callback();
        });

        resizeObserver.observe(elem);

        return true;
    },

    getItemSize: containerId => {
        const elem = document.getElementById(containerId);
        const result = [elem?.offsetHeight || 0, elem?.offsetWidth || 0];
        return result;
    },

    getWidestItemInContainer: containerId => {
        const elem = document.getElementById(containerId);

        let result = '';
        let maxWidth = -1;

        for (let cn of elem.childNodes) {
            if (!cn || !cn.offsetWidth) {
                continue;
            }
            let width = cn.offsetWidth;
            if (width > maxWidth) {
                maxWidth = width;
                result = cn.id;
            }
        }

        return result;
    }
};

const localStore = {
    AddToStore: (key, value) => {
        localStorage.setItem(key, value);
    },
    GetFromStore: (key) => {
        return localStorage.getItem(key);
    },
    DeleteFromStore: (key) => {
        localStorage.removeItem(key, null);
    },
    GetAllKeys: () => {
        var keys = []
        for (let i = 0; i < localStorage.length; i++)
        {
            const key = localStorage.key(i);
            keys.push(key);
        }
        return keys;
    }
}

window.view = view;
window.localStore = localStore;